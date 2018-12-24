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
    public class TypeJointsController : Controller
    {
        private readonly IMapper _map;
        private readonly ITypeJointRepository _typeJointRepository;

        public TypeJointsController(ITypeJointRepository typeJointRepository, IMapper map)
        {
            _typeJointRepository = typeJointRepository;
            _map = map;
        }

        public IActionResult Index()
        {
            return View();
        }

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

            (IEnumerable<TypeJoint> data, int totals, int filter) = await _typeJointRepository.GetTable(record, start, search, orderId, orderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id, bool update = false)
        {
            TypeJointViewModel model = new TypeJointViewModel();

            if (update)
            {
                var tbl = await _typeJointRepository.GetByMa(id);
                model = _map.Map<TypeJointViewModel>(tbl);
                model.Update = update;
            }

            return PartialView("_TypeJointView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] TypeJointViewModel model)
        {
            string user = HttpContext.User.Identity.Name;
            TypeJoint tbl = new TypeJoint();
            bool status;
            if (model.Update)
            {
                tbl = await _typeJointRepository.GetByMa(model.Type);

                _map.Map<TypeJointViewModel, TypeJoint>(model, tbl);
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
                status = await _typeJointRepository.Update(tbl);
                if (status)
                {
                    return Ok("Cập nhật thành công");
                }
                return BadRequest("Lỗi cập nhật");
            }
            _map.Map<TypeJointViewModel, TypeJoint>(model, tbl);
            tbl.UserCreated = user;
            tbl.DateCreated = DateTime.Now;
            status = await _typeJointRepository.Save(tbl);
            if (status)
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Lỗi thêm");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool status = await _typeJointRepository.Delete(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Lỗi xóa");
        }

        [HttpGet]
        public async Task<IActionResult> Find(string id)
        {
            var tbl = await _typeJointRepository.Find(id);
            return Json(tbl);
        }
    }
}