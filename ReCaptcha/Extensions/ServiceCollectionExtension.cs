using System;
using Microsoft.Extensions.DependencyInjection;
using ReCaptcha.Options;

namespace ReCaptcha.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRecaptcha(this IServiceCollection services,
            Action<RecaptchaOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException($"Recaptcha options (SiteKey - SecretKey) not specified.");
            }

            //RecaptchaOptions config
            services.Configure(options);
            
            services.AddTransient<IRecaptchaVerifier, RecaptchaVerifier>();
            
            //TODO: create RecaptchaClientConsts?
            //IHttpClientFactory service registration
            services.AddHttpClient("recaptcha", client =>
            {
                client.BaseAddress = new Uri("https://www.google.com/");
            });

            return services;
        }
    }
}