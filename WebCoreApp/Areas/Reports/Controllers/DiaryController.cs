using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCoreApp.Service.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebCoreApp.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class DiaryController : Controller
    {
        private readonly IReportDiaryRepository _reportDiaryRepository;

        public DiaryController(IReportDiaryRepository reportDiaryRepository)
        {
            _reportDiaryRepository = reportDiaryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ByBp(DateTime dateFrom, DateTime dateTo)
        {
            var data = await _reportDiaryRepository.GetData(dateFrom, dateTo)
                .GroupBy(m => m.Ten_phong)
                .Select(g => new
                {
                    Ten_Phong = g.Key,
                    Total_Date = g.Where(m => m.Total_Date>0).Count(),
                    Total_Work = g.Sum(m => m.Total_Work)
                }).ToListAsync();
            return Ok(data);
        }
    }
}