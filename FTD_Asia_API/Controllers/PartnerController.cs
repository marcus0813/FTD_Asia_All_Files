using FTD_Asia_API.Data;
using FTD_Asia_API.Models;
using FTD_Asia_API.Models.Request;
using FTD_Asia_API.Models.Response;
using Microsoft.AspNetCore.Mvc;
using log4net;
using FTD_Asia_API.Interface;
using static FTD_Asia_API.Entities.Enums;
using FTD_Asia_API.Entities;

namespace FTD_Asia_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class PartnerController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PartnerController));
        private readonly IValidationService _validationService;
        private readonly IDiscountRulesService _discountRulesService;

        public PartnerController(IValidationService validationService, IDiscountRulesService discountRulesService)
        {
            _validationService = validationService;
            _discountRulesService = discountRulesService;
        }

        [HttpPost("submittrxmessage")]
        public PartnerResponse SubmitTrxMessage(PartnerRequest request)
        {
            PartnerResponse result = new PartnerResponse();

            try
            {
                //check Authorization
                bool isAuthorizedPartner = _validationService.IsAuthorizedUser(request);

                if (!isAuthorizedPartner)
                    throw new Exception("Access Denied!");

                //check Valid Total Amount
                bool isValidTotalAmount = _validationService.IsValidTotalAmount(request.TotalAmount, request.Items);

                if (!isValidTotalAmount)
                    throw new Exception("Invalid Total Amount.");

                //check Valid Timestamp
                bool isValidTimestamp = _validationService.IsValidTimestamp(request.Timestamp);

                if (!isValidTimestamp)
                    throw new Exception("Expired.");

                //Proceed to Calculcate Discount Price
                DiscountedItem discountedItem = _discountRulesService.GetDiscountedItem(request.TotalAmount);

                result.Result = (int)ResultType.Success;
                result.TotalAmount = request.TotalAmount;
                result.TotalDiscount = discountedItem.TotalDiscount;
                result.FinalAmount = discountedItem.FinalAmount;
            }
            catch (Exception ex)
            {
                result.Result = (int)ResultType.Failed;
                result.ResultMessage = ex.Message;
            }

            return result;
        }
    }
}