using FTD_Asia_Test.Controllers;
using log4net;
using FTD_Asia_Test.Interface;
using FTD_Asia_Test.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FTD_Asia_Test.Services;
using Moq;
using Microsoft.Extensions.Configuration;
using FTD_Asia_Test.Data;
using FTD_Asia_Test.Entities;
using Newtonsoft.Json;

namespace FTD_Asia_Unit_Test
{
    [TestClass()]
    public class FTD_Asia_UT
    {
        public TestContext TestContext { get; set; }

        private IConfiguration _config;

        private PartnerRequest request = new PartnerRequest();

        [TestInitialize]
        public void Setup()
        {
            _config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            request = new PartnerRequest()
            {
                PartnerKey = "FAKEPEOPLE",
                PartnerRefNo = "FG-00002",
                PartnerPassword = "RkFLRVBBU1NXT1JENDU3OA==",
                TotalAmount = 100000,
                //Timestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'"), to get timestamp
                Timestamp = "2025-06-26T18:20:22.0000000Z",
                Sig = "ZDgwMTU5NjhiODYxYjBkMGFhY2M0ZDEyODIwOWEyMDM3YjMwNWZhMjRkMGIyYjJjNzUxNzgxZjczMjcyODhjOQ==",
            };

            TestContext.WriteLine($"Request : {JsonConvert.SerializeObject(request)}");
        }

        [TestMethod]
        public void GetSignatureString()
        {
            var partnerRepository = new PartnerRepository(_config);
            var validationService = new ValidationService(_config, partnerRepository);
            string result = validationService.GetEncryptedSignature(request);
            TestContext.WriteLine($"Response : {result}");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void CheckUserIsAuthorized()
        {
            var partnerRepository = new PartnerRepository(_config);
            var validationService = new ValidationService(_config, partnerRepository);
            bool result = validationService.IsAuthorizedUser(request);
            TestContext.WriteLine($"Response : {result}");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckTotalAmountIsValid()
        {
            var partnerRepository = new PartnerRepository(_config);
            var validationService = new ValidationService(_config, partnerRepository);
            bool result = validationService.IsValidTimestamp(request.Timestamp);
            TestContext.WriteLine($"Response : {result}");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckTimestampIsValid()
        {
            var partnerRepository = new PartnerRepository(_config);
            var validationService = new ValidationService(_config, partnerRepository);
            bool result = validationService.IsValidTotalAmount(request.TotalAmount, request.Items);
            TestContext.WriteLine($"Response : {result}");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetDiscountedPrice()
        {
            var discountRulesService = new DiscountRulesService();
            DiscountedItem result = discountRulesService.GetDiscountedItem(request.TotalAmount);
            TestContext.WriteLine($"Response : {JsonConvert.SerializeObject(result)}");
            Assert.IsTrue(result.FinalAmount > 0);
        }
    }
}