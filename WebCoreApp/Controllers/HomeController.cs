using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCoreApp.Constants;
using WebCoreApp.Models;

namespace WebCoreApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [Authorize(AuthenticationSchemes = "Identity.Application",Policy ="View;Home")]
        public IActionResult Index()
        {
          
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Notification()
        {
            return ViewComponent("Notification");
        }
    }
}