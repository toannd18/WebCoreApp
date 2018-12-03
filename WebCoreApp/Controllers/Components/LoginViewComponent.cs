using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Controllers.Components
{
    public class LoginViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        public LoginViewComponent(UserManager<AppUser> userManager)
        {
            this.userManager= userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var model = await userManager.FindByIdAsync(userId);
            
            return View("_Login",model);
        }
    }
}
