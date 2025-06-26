using FTD_Asia_API.Entities;
using FTD_Asia_API.Interface;
using FTD_Asia_API.Models.Response;
using System.Text.Json;

namespace FTD_Asia_API.Data
{
    public class PartnerRepository: IPartnerRepository
    {
        private readonly IConfiguration _configuration;

        public PartnerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<PartnerInfo> GetPartnerInfoList() {
            string filePath = _configuration["CustomParams:MockDataFilePath"];
            var json = File.ReadAllText(filePath);
            List<PartnerInfo> partnerInfos = JsonSerializer.Deserialize<List<PartnerInfo>>(json);
            return partnerInfos;
        }
    }
}
