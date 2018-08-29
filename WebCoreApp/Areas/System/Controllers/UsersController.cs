using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Constants;
using DataContext.WebCoreApp;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Areas.System.Controllers
{
    [Area("System")]
    
    public class UsersController : Controller
    {
        

        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
        {
            _userService = userService;
            
    }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        // GET: Users
        public IActionResult Index()
        {
            return View();
        }

        [Route("[area]/api/[controller]/load")]
        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> Load(int? draw, int? length, int start, string search)
        {
            if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
            {
                search = Request.Form["search[value]"];
            }
            search = Request.Form["search[value]"];
            string oderId = Request.Form["order[0][column]"];
            string oderDir = Request.Form["order[0][dir]"];

            int record = length ?? 5;
            if (draw == null)
            {
                start = (start - 1) * record;
            }

            (IEnumerable<AppUser> data, int totals, int filter) = await _userService.GetTable(start, record, oderId, oderDir, search);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

   
    
    }
}