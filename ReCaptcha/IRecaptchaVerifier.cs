using System.Threading.Tasks;

namespace ReCaptcha
{
    public interface IRecaptchaVerifier
    {
        Task<RecaptchaResponse> VerifyAsync(string response, string? remoteIp = null);
    }
}