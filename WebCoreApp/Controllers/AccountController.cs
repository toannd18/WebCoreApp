using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreApp.Constants;
using WebCoreApp.Enume;
using WebCoreApp.Models;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userService;
        

        public AccountController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View("LoginAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.IsUser(model.UserName, model.PassWord);
                if (result == Status.Successed)
                {
                    var tbl = await _userService.GetByMa(model.UserName);
                    string display = tbl.MaCvNavigation.Display.ToString();
                    string to = tbl.MaTo == null ? "" : tbl.MaTo;
                    List<Claim> claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, model.UserName),
                         new Claim("Ma_BP",tbl.MaBp),
                         new Claim("Ma_To",to),
             
                         new Claim("Display",display),
                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // create principal
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // sign-in
                    await HttpContext.SignInAsync(
                            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                            principal: principal,
                            new AuthenticationProperties
                            {
                                IsPersistent = model.Remember
                            });

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Mật khẩu hoặc Tài khoản không chính xác");
            }

            return View("LoginAdmin",model);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        
        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}