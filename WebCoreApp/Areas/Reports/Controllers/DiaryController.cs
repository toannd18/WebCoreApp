using DataContext.Drapper.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class DiaryController : Controller
    {
        private readonly IBPRepository _bPRepository;
        private readonly IReportService _reportService;
        private readonly IWareHouseService _wareHouseService;

        public DiaryController(IBPRepository bPRepository,
                             IReportService reportService, IWareHouseService wareHouseService)
        {
            _bPRepository = bPRepository;
            _reportService = reportService;
            _wareHouseService = wareHouseService;
        }

        public async Task<IActionResult> Index()
        {
            var listBP = await _bPRepository.GetAll();
            ViewBag.ListBP = new SelectList(listBP.Where(m => m.MaBp != "BGD").ToList(), "MaBp", "TenBp");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ByBp(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var data = await _reportService.GetTotalBpAsync(dateFrom, dateTo);
                data = data.Where(m => m.Ma_BP != "BGD").ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ByTo(DateTime dateFrom, DateTime dateTo, string MaBp)
        {
            var data = await _reportService.GetTotalBpAsync(dateFrom, dateTo);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Test(string MaNhom)
        {
            var data = await _wareHouseService.GetDMVT(MaNhom);
            return Ok(data);
        }
    }
}