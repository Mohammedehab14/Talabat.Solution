using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.IRepositories;
using Talabat.Repo.Repositories;

namespace Talabat.APIs.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));

            Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var Errs = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(E => E.ErrorMessage).ToList();
                    var ValidationErrRespons = new ApiValidationError()
                    {
                        Errors = Errs
                    };
                    return new BadRequestObjectResult(ValidationErrRespons);
                };
            });
            return Services;
        }
    }
}
