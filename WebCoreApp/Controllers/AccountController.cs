using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebCoreApp.Extensions.Email;
using WebCoreApp.Infrastructure.Interfaces;
using WebCoreApp.Models;

namespace WebCoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration Configuration;
        private readonly IEmailService _emailService;

        public AccountController(
            ILogger<AccountController> logger, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IPositionRepository positionRepository,
            IConfiguration configuration, IEmailService emailService)
        {
            _positionRepository = positionRepository;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View("LoginAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
           
            returnUrl = returnUrl ?? Url.Content("~/Home/Index");
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.PassWord, model.Remember, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Tài khoản đã được đăng nhập");
                    await UpdateClaim(user);
                    return LocalRedirect(returnUrl);
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogInformation("Tài khoản đã bị khóa");
                    ModelState.AddModelError(string.Empty, "Tài khoản này đã bị khóa");
                    return View("LoginAdmin");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản này không chính xác");
                    return View("LoginAdmin");
                }
            }
            return View("LoginAdmin", model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginToken([FromBody] LoginModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.PassWord, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = User.Claims;

                DateTime dateTime = DateTime.Now.AddDays(14);
                string configKey = Configuration["JwtConfig:Key"];
                string configIssuer = Configuration["JwtConfig:ValidAudience"];
                string configAudience = Configuration["JwtConfig:ValidIssuer"];
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
                SigningCredentials sigInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                JwtSecurityToken tokenstring = new JwtSecurityToken(
                                    issuer: configIssuer,
                                    audience: configAudience,
                                    claims: claims,
                                    expires: dateTime,
                                    signingCredentials: sigInCred
                                    );
                string token_key = new JwtSecurityTokenHandler().WriteToken(tokenstring);
                var token = new
                {
                    access_token = token_key,
                    expires_in = tokenstring.ValidTo
                };
                return Ok(token);
            }
            return NotFound("Error");
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReset(ResetPassword model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản này không tồn tại");
                    return View("ResetPassword");
                }
                if (!user.LockoutEnabled)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản này đã bị khóa");
                    return View("ResetPassword");
                }
                if (user.Email == model.Email)
                {
                    ModelState.AddModelError(string.Empty, "Địa chỉ email không chính xác");
                    return View("ResetPassword");
                }
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Page(
                    pageName: null,
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailService.SendAsync("Khôi phục mật khẩu",
                    $"Vui lòng xác nhận khôi phục mật khẩu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>bấm vào đường link</a>.", model.Email);

                ViewBag.Message = "Xác nhận email để khôi phục mật khẩu";
                return View("ResetPassword");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra vui lòng liên hệ với administrator");
                return View("ResetPassword");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmReset(string userId,string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, code, "Ptscqng1234");
            if (result.Succeeded)
            {
                ViewBag.Message = "Khôi phục mật khẩu thành công";
                return View("LoginAdmin");
            }
            ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra vui lòng liên hệ với administrator");
            return View("LoginAdmin");
        }
        #region Create Claim List

        private async Task<IList<Claim>> UpdateClaim(AppUser user)
        {
            IList<Claim> claim = await _userManager.GetClaimsAsync(user);
            if (claim.Count == 0)
            {
                var cv = await _positionRepository.GetByMa(user.MaCv);
                var roles = await _userManager.GetRolesAsync(user);
                int display = 1;
                if (cv != null && cv.Display.HasValue)
                {
                    display = cv.Display.Value;
                }

                claim.Add(new Claim("Ma_Bp", user.MaBp ?? ""));
                claim.Add(new Claim("Ma_To", user.MaTo ?? ""));
                claim.Add(new Claim("Display", display.ToString()));

                await _userManager.AddClaimsAsync(user, claim);
            }
            claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claim.Add(new Claim(ClaimTypes.Name, user.UserName));
            return claim;
        }

        #endregion Create Claim List
    }
}