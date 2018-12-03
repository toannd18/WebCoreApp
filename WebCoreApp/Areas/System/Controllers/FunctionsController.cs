using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Areas.System.Controllers
{
    [Area("System")]
    public class FunctionsController : Controller
    {
        private readonly IFunctionRepository functionRepository;

        public FunctionsController(IFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
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
            var tbl = await functionRepository.GetAll();
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
    }
}