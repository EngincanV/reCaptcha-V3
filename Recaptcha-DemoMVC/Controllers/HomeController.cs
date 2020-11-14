using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReCaptcha;
using Recaptcha_DemoMVC.Models;

namespace Recaptcha_DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecaptchaVerifier _recaptchaVerifier;

        public HomeController(ILogger<HomeController> logger, IRecaptchaVerifier recaptchaVerifier)
        {
            _logger = logger;
            _recaptchaVerifier = recaptchaVerifier;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RecaptchaModel model)
        {
            var recaptcha = await _recaptchaVerifier.VerifyAsync(model.RecaptchaToken);

            if (recaptcha.Success && recaptcha.Score >= 0.7)
            {
                return Ok(recaptcha);
            }

            return BadRequest(new {message = "You can not proceed this action "});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}