using FTD_Asia_Test.Data;
using FTD_Asia_Test.Interface;
using FTD_Asia_Test.Services;

namespace FTD_Asia_Test.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Add Services
            services.AddScoped<IDiscountRulesService, DiscountRulesService>();
            services.AddScoped<IValidationService, ValidationService>();

            //Add Repository
            services.AddScoped<IPartnerRepository, PartnerRepository>();

            return services;
        }
    }
}
