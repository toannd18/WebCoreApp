using AutoMapper;
using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Areas.Pipes.Models;
using WebCoreApp.Infrastructure.Interfaces.Pipes;
using WebCoreApp.Infrastructure.ViewModels.Pipe;

namespace WebCoreApp.Areas.Pipes.Controllers
{
    [Area("Pipes")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class IsometricsController : Controller
    {
        private readonly IMapper _map;
        private readonly IIsometricRepository _isometricRepository;
        private readonly IIsoJointRepository _isoJointRepository;

        public IsometricsController(IIsometricRepository isometricRepository, IMapper map, IIsoJointRepository isoJointRepository)
        {
            _isometricRepository = isometricRepository;
            _map = map;
            _isoJointRepository = isoJointRepository;
        }

        public IActionResult Index()
        {
            var model = new JointViewModel();
            return View(model);
        }

        #region Bản vẽ

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

            (IEnumerable<Isometric> data, int totals, int filter) = await _isometricRepository.GetTable(record, start, search, orderId, orderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id, bool update = false)
        {
            IsometricViewModel model = new IsometricViewModel();

            if (update)
            {
                var tbl = await _isometricRepository.GetByMa(id);
                model = _map.Map<IsometricViewModel>(tbl);
            }
            model.Update = update;
            return PartialView("_IsometricView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] IsometricViewModel model)
        {
            string user = HttpContext.User.Identity.Name;
            Isometric tbl = new Isometric();
            bool status;

            if (model.Update)
            {
                tbl = await _isometricRepository.GetByMa(model.DrawName);
                _map.Map<IsometricViewModel, Isometric>(model, tbl);
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
                status = await _isometricRepository.Update(tbl);
                if (status)
                {
                    return Ok("Cập nhật thành công");
                }
                return BadRequest("Lỗi cập nhật");
            }

            string idProject = HttpContext.User.FindFirst("Display").Value;
            _map.Map<IsometricViewModel, Isometric>(model, tbl);
            tbl.UserCreated = user;
            tbl.DateCreated = DateTime.Now;
            status = await _isometricRepository.Save(tbl);

            if (status)
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Lỗi thêm");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool status = await _isometricRepository.Delete(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Lỗi xóa");
        }

        #endregion Bản vẽ

        #region danh sách mối hàn

        [HttpGet]
        public async Task<IActionResult> LoadJoint(string search, string isoName)
        {
            (IEnumerable<IsoJoint> data, int totals, int filter) = await _isoJointRepository.GetTable(0, 1, search, null, null, isoName);

            return Ok(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveJoint([FromBody] JointViewModel model)
        {
            IsoJoint tbl = new IsoJoint();
            bool status;
            string user = HttpContext.User.Identity.Name;
            if (!model.UpdateJoint)
            {
                _map.Map<JointViewModel, IsoJoint>(model, tbl);
                tbl.Id = Guid.NewGuid();

                tbl.DateCreated = DateTime.Now;
                tbl.UserCreated = user;
                status = await _isoJointRepository.Save(tbl);
                if (status)
                {
                    return Ok("Thêm thành công");
                }
                return BadRequest("Lỗi thêm");
            }
             tbl = await _isoJointRepository.GetByMa(model.Id);
            _map.Map<JointViewModel, IsoJoint>(model, tbl);
            tbl.DateUpdated = DateTime.Now;
            tbl.UserUpdated = user;
            status = await _isoJointRepository.Update(tbl);
            if (status)
            {
                return Ok("Cập nhât thành công");
            }
            return BadRequest("Lỗi cập nhật");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJoint(string id)
        {
            Guid.TryParse(id, out Guid ma);
            var status = await _isoJointRepository.Delete(ma);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Lỗi xóa");
        }

        #endregion danh sách mối hàn
    }
}