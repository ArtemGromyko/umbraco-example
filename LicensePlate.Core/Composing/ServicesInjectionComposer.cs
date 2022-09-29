using LicensePlate.Core.Infrastructure.DependencyInjections;
using Umbraco.Cms.Core.Composing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using LicensePlate.Core.Infrastructure.Validators;

namespace LicensePlate.Core.Composing
{
    public class ServicesInjectionComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddAuthentificationOptions(builder.Config);
            builder.Services.AddCustomServices(builder.Config);
            builder.Services.AddFilters();

            builder.Services.AddMediatR(typeof(ServicesInjectionComposer).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(ServicesInjectionComposer).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddAutoMapper(typeof(ServicesInjectionComposer));
        }
    }
}