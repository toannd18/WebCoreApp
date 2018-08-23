using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Controllers.Components
{
    public class LoginViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        public LoginViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string user = HttpContext.User.Identity.Name.ToString();
            var model = await _userRepository.GetByMa(user);
            
            return View("_Login",model);
        }
    }
}
