using FTD_Asia_API.Controllers;
using FTD_Asia_API.Entities;
using FTD_Asia_API.Interface;
using FTD_Asia_API.Models.Request;
using log4net;
using System.Security.Cryptography;
using System.Text;

namespace FTD_Asia_API.Services
{
    public class ValidationService : IValidationService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ValidationService));
        private readonly IConfiguration _configuration;
        private readonly IPartnerRepository _partnerRepository;

        public ValidationService(IConfiguration configuration, IPartnerRepository partnerRepository)
        {
            _configuration = configuration;
            _partnerRepository = partnerRepository;
        }

        public bool IsAuthorizedUser(PartnerRequest request)
        {
            //check partner exist 
            List<PartnerInfo> partnerInfos = _partnerRepository.GetPartnerInfoList();

            PartnerInfo allowedPartner = partnerInfos.Find(x => x.PartnerRefNo == request.PartnerRefNo && x.PartnerKey == request.PartnerKey);

            if (allowedPartner == null)
                return false;

            //check password exist
            string passwordFromServer = EncodeToBase64String(allowedPartner.PartnerPassword);

            if (passwordFromServer != request.PartnerPassword)
                return false;

            //signature check 

            string signFromServer = GetEncryptedSignature(request);

            if (signFromServer != request.Sig)
                return false;

            //All checking Passed
            return true;
        }

        public bool IsValidTotalAmount(long totalAmount, List<ItemDetail> itemDetails)
        {
            if (itemDetails == null)
                return true;

            long totalAmountServer = 0;

            foreach (var item in itemDetails)
            {
                totalAmountServer += (item.UnitPrice * item.Qty);
            }

            if (totalAmountServer != totalAmount)
                return false;
            else
                return true;
        }

        public bool IsValidTimestamp(string timeStamp)
        {
            int expiredDurationInMinutes = int.Parse(_configuration["CustomParams:ExpiredDurationInMinutes"]);

            if (!DateTimeOffset.TryParse(timeStamp, out DateTimeOffset dateTimeFromTimeStamp))
            {
                return false;
            }

            DateTime serverDateTime = DateTime.Now;

            TimeSpan difference = serverDateTime - dateTimeFromTimeStamp.UtcDateTime;

            return Math.Abs(difference.TotalMinutes) <= expiredDurationInMinutes;
        }

        public string GetEncryptedSignature(PartnerRequest request)
        {
            string result = string.Empty;

            string formattedSignature = FormatSignatureString(request.Timestamp, request.PartnerKey, request.PartnerRefNo, request.TotalAmount, request.PartnerPassword);

            byte[] bytes = Encoding.UTF8.GetBytes(formattedSignature);

            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes);

                var sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                string lowercaseSHA256 = sb.ToString();

                result = EncodeToBase64String(lowercaseSHA256);
            }
            return result;
        }

        private string FormatSignatureString(string timestamp, string partnerkey, string partnerRefNo, long totalAmount, string password)
        {
            var SigTimestampFormat = _configuration["CustomParams:SigTimestampFormat"];

            var dateUtcValue = DateTimeOffset.Parse(timestamp);
            timestamp = dateUtcValue.UtcDateTime.ToString(SigTimestampFormat);

            return $"{timestamp}{partnerkey}{partnerRefNo}{totalAmount}{password}";
        }

        private string EncodeToBase64String(string inputString)
        {
            byte[] hexAsBytes = Encoding.UTF8.GetBytes(inputString);

            return Convert.ToBase64String(hexAsBytes);
        }
    }
}
