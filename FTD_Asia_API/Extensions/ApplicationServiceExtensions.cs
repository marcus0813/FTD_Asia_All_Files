using FTD_Asia_API.Data;
using FTD_Asia_API.Interface;
using FTD_Asia_API.Services;

namespace FTD_Asia_API.Extensions
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
