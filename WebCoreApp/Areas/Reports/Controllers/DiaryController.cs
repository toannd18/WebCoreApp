using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCoreApp.Service.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebCoreApp.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class DiaryController : Controller
    {
        private readonly IReportDiaryRepository _reportDiaryRepository;
        private readonly IBPRepository _bPRepository;

        public DiaryController(IReportDiaryRepository reportDiaryRepository, IBPRepository bPRepository)
        {
            _reportDiaryRepository = reportDiaryRepository;
            _bPRepository = bPRepository;
        }

        public async Task<IActionResult> Index()
        {
            var listBP = await _bPRepository.GetAll();
            ViewBag.ListBP = new SelectList(listBP.Where(m=>m.MaBp!="BGD").ToList(),"MaBp", "TenBp");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ByBp(DateTime dateFrom, DateTime dateTo)
        {
            var data = await _reportDiaryRepository.GetData(dateFrom, dateTo)
                .GroupBy(n => n.MaBP)
                .Select(g => new
                {
                    Ten_Phong = g.Select(m => m.Ten_phong).FirstOrDefault(),
                    Total_Date = g.Where(m => m.Total_Date > 0).Count(),
                    Total_Work = g.Sum(m => m.Total_Work)
                }).ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> ByTo(DateTime dateFrom, DateTime dateTo,string MaBp)
        {
            var data = await _reportDiaryRepository.GetData(dateFrom, dateTo)
                .Where(m => m.MaBP == MaBp && !string.IsNullOrEmpty(m.MaTo))
                .GroupBy(n => n.MaTo)
                .Select(g => new
                {
                    Ten_To = g.Select(m => m.Ten_to).FirstOrDefault(),
                    Total_Date=g.Where(m=>m.Total_Date>0).Count(),
                    Total_Work=g.Sum(m=>m.Total_Work)
                })
                .ToListAsync();
            return Ok(data);
        }
    }
}