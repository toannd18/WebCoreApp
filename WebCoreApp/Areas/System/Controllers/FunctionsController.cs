using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;
using WebCoreApp.Infrastructure.ViewModels.System;

namespace WebCoreApp.Areas.System.Controllers
{
    [Area("System")]
    public class FunctionsController : Controller
    {
        private readonly IFunctionRepository functionRepository;
        private readonly RoleManager<AppRole> roleManager;

        public FunctionsController(IFunctionRepository functionRepository, RoleManager<AppRole> roleManager)
        {
            this.functionRepository = functionRepository;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFunction()
        {
            var tbl = await functionRepository.GetAll();
            return Ok(tbl);
        }

        public async Task<IActionResult> GetFunctionDetail(string Id = null)
        {
            var tbl = await functionRepository.GetAllId();
            ViewBag.ListParent = new SelectList(tbl, "Id", "Name");
            var model = await functionRepository.GetByMa(Id);
            return PartialView("_DetailFunction", model);
        }

        [Route("[area]/[controller]/SaveFunction/{update}")]
        [HttpPost]
        public async Task<IActionResult> SaveFunction(bool update, [FromBody] Functions tbl)
        {
            bool status;
            if (string.IsNullOrEmpty(tbl.ParentId)) tbl.ParentId = null;
            status = await (update ? functionRepository.Update(tbl) : functionRepository.Save(tbl));
            return Ok(status);
        }

        [Route("[area]/[controller]/DeleteFunction/{ma}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFunction(string ma)
        {
            bool status = await functionRepository.Delete(ma);
            return Ok(status);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetRole(string Id)
        {
            var role = await roleManager.Roles.ToListAsync();
            ViewBag.ListRole = new SelectList(role.Where(m=>m.Name !="Admin"), "Id", "Name");

            return PartialView("_RoleFunction");
        }

        public async Task<IActionResult> LoadPermission(string id, string idFunc)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest("Không có role này");
            }
            var ListClaim = await roleManager.GetClaimsAsync(role);
            ListClaim = ListClaim.Where(m => m.Type.ToString() == idFunc).ToList();
            return Ok(ListClaim);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePermission(CustomPermissionViewModel model)
        {
            try
            {
                await functionRepository.UpdatePermission(model);
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}