using AutoMapper;
using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;
using WebCoreApp.Infrastructure.ViewModels.Pipe;

namespace WebCoreApp.Areas.Pipes.Controllers
{
    [Area("Pipes")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class WeldingsController : Controller
    {
        private readonly IIsoJointRepository _isoJointRepository;
        private readonly IMapper _map;

        public WeldingsController(IIsoJointRepository isoJointRepository, IMapper map)
        {
            _isoJointRepository = isoJointRepository;
            _map = map;
        }

        public IActionResult Index()
        {
            WeldingViewModel model = new WeldingViewModel();
            return View(model);
        }

        public async Task<IActionResult> WeldedLoad(string isoName, bool welding)
        {
            var data = await _isoJointRepository.GetWelding(isoName, welding);
            return Ok(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] WeldingViewModel model)
        {
            var tbl = await _isoJointRepository.GetByMa(model.Id);
            DateTime? date = tbl.WeldingDate;
            if (date.HasValue)
            {
                if (date.Value.AddDays(3) < DateTime.Today)
                {
                    return BadRequest("Đã quá 3 ngày");
                }
            }
            _map.Map<WeldingViewModel, IsoJoint>(model, tbl);

            bool status = await _isoJointRepository.Update(tbl);

            if (status)
            {
                return Ok("Lưu thành công");
            }
            return BadRequest("Lỗi lưu");
        }

        [HttpPost]
        public async Task<IActionResult> ClearWelding(Guid id)
        {
            var tbl = await _isoJointRepository.GetByMa(id);
            DateTime? date = tbl.WeldingDate;
            if (date.HasValue)
            {
                if (date.Value.AddDays(3) < DateTime.Today)
                {
                    return BadRequest("Đã quá 3 ngày");
                }
            }
            tbl.WeldingDate = null;
            tbl.Welder1 = null;
            tbl.Welder2 = null;
            tbl.Welder3 = null;
            tbl.Welder4 = null;
            tbl.Heate1 = null;
            tbl.Heate2 = null;
            bool status = await _isoJointRepository.Update(tbl);
            if (status)
            {
                return Ok("Xóa ngày hàn thành công");
            }
            return BadRequest("Lỗi xóa ngày hàn");
        }
    }
}