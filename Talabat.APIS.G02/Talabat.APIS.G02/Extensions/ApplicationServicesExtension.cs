using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.G02.Errors;
using Talabat.APIS.G02.Helpers;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.APIS.G02.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AppServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            
            Services.AddAutoMapper(typeof(MappingProfile));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(p => p.ErrorMessage).ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });

            return Services;
        }
    }
}
