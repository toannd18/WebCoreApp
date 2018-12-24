using AutoMapper;
using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;
using WebCoreApp.Infrastructure.ViewModels.Pipe;

namespace WebCoreApp.Areas.Pipes.Controllers
{
    [Area("Pipes")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class WeldersController : Controller
    {
        private readonly IWelderRepository _welderRepository;
        private readonly IMapper _mapper;

        public WeldersController(IWelderRepository welderRepository, IMapper mapper)
        {
            _welderRepository = welderRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

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

            (IEnumerable<Welder> data, int totals, int filter) = await _welderRepository.GetTable(record, start, search, orderId, orderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id, bool update = false)
        {
            WelderViewModel model = new WelderViewModel();
            if (update)
            {
                var tbl = await _welderRepository.GetByMa(id);
                model = _mapper.Map<WelderViewModel>(tbl);
                model.Update = update;
            }

            return PartialView("_WelderView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] WelderViewModel model)
        {
            bool status;
            var tbl = _mapper.Map<Welder>(model);
            if (model.Update)
            {
                status = await _welderRepository.Update(tbl);
                if (status)
                {
                    return Ok("Cập nhật thành công");
                }
                return BadRequest("Lỗi cập nhật");
            }
            status = await _welderRepository.Save(tbl);
            if (status)
            {
                return Ok("Lưu thành công");
            }
            return BadRequest("Lỗi thêm");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool status;
            status = await _welderRepository.Delete(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Lỗi xóa");
        }

        [HttpGet]
        public async Task<JsonResult> Find(string id)
        {
            var data = await _welderRepository.FindWelder(id);
            return Json(data);
        }
    }
}