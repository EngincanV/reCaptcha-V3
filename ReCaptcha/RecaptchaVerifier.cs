using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ReCaptcha.Options;

namespace ReCaptcha
{
    public class RecaptchaVerifier : IRecaptchaVerifier
    {
        private const string recaptchaVerifyUrl = "recaptcha/api/siteverify";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RecaptchaOptions _options;

        public RecaptchaVerifier(IHttpClientFactory httpClientFactory, IOptions<RecaptchaOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<RecaptchaResponse> VerifyAsync(string response)
        {
            var postBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _options.SecretKey),
                new KeyValuePair<string, string>("response", response),
            });

            var client = _httpClientFactory.CreateClient("recaptcha");
            var postResponse = await client.PostAsync(recaptchaVerifyUrl, postBody);

            var recaptchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(await postResponse.Content.ReadAsStringAsync());

            return recaptchaResponse;
        }
    }
}