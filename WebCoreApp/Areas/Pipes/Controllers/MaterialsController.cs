using AutoMapper;
using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;
using WebCoreApp.Infrastructure.ViewModels.Pipe;

namespace WebCoreApp.Areas.Pipes.Controllers
{
    [Area("Pipes")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class MaterialsController : Controller
    {
        private readonly IMaterialPipeRepository _materialPipeRepository;
        private readonly IMapper _map;

        public MaterialsController(IMaterialPipeRepository materialPipeRepository, IMapper map)
        {
            _materialPipeRepository = materialPipeRepository;
            _map = map;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Load(int? draw, int? length, int start, string search)
        //{
        //    if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
        //    {
        //        search = Request.Form["search[value]"];
        //    }
        //    string orderId = Request.Form["order[0][column]"];
        //    string orderDir = Request.Form["order[0][dir]"];
        //    int record = length ?? 10;

        //    if (draw == null)
        //    {
        //        start = (start - 1) * record;
        //    }

        //    (IEnumerable<MaterialPipe> data, int totals, int filter) = await _materialPipeRepository.GetTable(record, start, search, orderId, orderDir);

        //    return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        //}
        [HttpPost]
        public async Task<IActionResult> Load(int? draw, int? length, int start, string search)
        {
         
            if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
            {
               search = Request.Form["search[value]"];
            }
            string orderId = Request.Form["order[0][column]"];
            string orderDir = Request.Form["order[0][dir]"];
            int record = length ?? 10;

            if (draw == null)
            {
                start = (start - 1) * record;
            }

            (IEnumerable<MaterialPipe> data, int totals, int filter) = await _materialPipeRepository.GetTable(record, start, search, orderId, orderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id, bool update = false)
        {
            MaterialViewModel model = new MaterialViewModel();

            if (update)
            {
                var tbl = await _materialPipeRepository.GetByMa(id);
                model = _map.Map<MaterialViewModel>(tbl);
            }
            model.Update = update;
            return PartialView("_MaterialView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] MaterialViewModel model)
        {
            string user = HttpContext.User.Identity.Name;
            MaterialPipe tbl = new MaterialPipe();
            bool status;

            if (model.Update)
            {
                tbl = await _materialPipeRepository.GetByMa(model.Name);
                _map.Map<MaterialViewModel, MaterialPipe>(model, tbl);
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
                status = await _materialPipeRepository.Update(tbl);
                if (status)
                {
                    return Ok("Cập nhật thành công");
                }
                return BadRequest("Lỗi cập nhật");
            }

            _map.Map<MaterialViewModel, MaterialPipe>(model, tbl);
            tbl.UserCreated = user;
            tbl.DateCreated = DateTime.Now;
            status = await _materialPipeRepository.Save(tbl);
            if (status)
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Lỗi thêm");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool status = await _materialPipeRepository.Delete(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Lỗi xóa");
        }

        [HttpGet]
        public async Task<IActionResult> Find(string id)
        {
            var tbl = await _materialPipeRepository.Find(id);
            return Json(tbl);
        }
    }
}