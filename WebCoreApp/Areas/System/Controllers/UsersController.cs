using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Constants;
using WebCoreApp.Infrastructure.Enume;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Areas.System.Controllers
{
    [Area("System")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(IUserRepository userService,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
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

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> RestorePass(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            var removePass = await _userManager.RemovePasswordAsync(user);
            if (removePass.Succeeded)
            {
                var result = await _userManager.AddPasswordAsync(user, "Ptscqng1234");
                if (result.Succeeded)
                {
                    return Ok(true);
                }
            }
            return Ok(false);
        }

        // Get Roles
        [HttpGet]
        public async Task<IActionResult> GetRole(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            IList<string> roleName = await _userManager.GetRolesAsync(user);
            List<AppRole> roleList = new List<AppRole>();
            ViewBag.ListRole = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

            foreach (var item in roleName)
            {
                roleList.Add(await _roleManager.FindByNameAsync(item));
            }
            return PartialView("_RoleView", roleList);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string roleName)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            IList<string> listRole = await _userManager.GetRolesAsync(user);
            bool existRole = listRole.Any(m => m == roleName);
            if (!existRole)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    AppRole role = await _roleManager.FindByNameAsync(roleName);
                    return Ok(new { status = true, role });
                }
                return Ok(false);
            }
            return BadRequest("Role này đã tồn tại");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            Guid user = Guid.Parse(userId);
            AppRole role = await _roleManager.FindByNameAsync(roleName);
            var result = await _userService.RemoveFromRole(user, role.Id);
            if (result == Status.Successed)
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}