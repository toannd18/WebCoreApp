using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Constants;
using WebCoreApp.Infrastructure.Interfaces;
using WebCoreApp.Infrastructure.ViewModels.Diary;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Areas.Diaries.Controllers
{
    [Area("Diaries")]
    public class RequestsController : Controller
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IBPRepository _bPRepository;
        private readonly IToRepository _toRepository;

        public RequestsController(IDiaryRepository diaryRepository,
            IBPRepository bPRepository,
            IToRepository toRepository)
        {
            _diaryRepository = diaryRepository;
            _bPRepository = bPRepository;
            _toRepository = toRepository;
        }

        #region User make report

        [Authorize(AuthenticationSchemes = "Identity.Aplication")]
        // Get Request
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Load table
        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/load")]
        [HttpPost]
        public async Task<IActionResult> Load(int? draw, int? length, int start, string search)
        {
            if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
            {
                search = Request.Form["search[value]"];
            }
           
            string oderId = Request.Form["order[0][column]"];
            string oderDir = Request.Form["order[0][dir]"];

            int record = length ?? 10;
            if (draw == null)
            {
                start = (start - 1) * record;
            }

            (IEnumerable<ViewDiaryModel> data, int totals, int filter) = await _diaryRepository.UserGetTable(record, start, search, oderId, oderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Get(int id = 0)
        {
            TblDaily tbl = new TblDaily();
            tbl.Date = DateTime.Today;
            if (id > 0)
            {
                tbl = await _diaryRepository.GetByMa(id);
            }
            return PartialView("_Dairy", tbl);
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/save")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]TblDaily tbl)
        {
            bool status = await (tbl.Id > 0 ? _diaryRepository.Update(tbl) : _diaryRepository.Save(tbl));
            return Ok(new { status = status });
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool status = await _diaryRepository.Delete(id);
            return Ok(new { status = status });
        }

        #endregion User make report

        #region User evaluate

        [Authorize(AuthenticationSchemes = "Identity.Aplication")]
        [HttpGet]
        public async Task<IActionResult> Evaluate()
        {
            string to = HttpContext.User.FindFirst("Ma_To").Value;
            string bp = HttpContext.User.FindFirst("Ma_BP").Value;
            int display = int.Parse(HttpContext.User.FindFirst("Display").Value);
            var tblTo = await _toRepository.GetAll();
            var tblBP = await _bPRepository.GetAll();
            if (display < 6)
            {
                tblTo = tblTo.Where(m => m.MaBp.Contains(bp)).ToList();
                tblBP = tblBP.Where(m => m.MaBp.Contains(bp)).ToList();
            }
            ViewBag.To = new SelectList(tblTo, "MaTo", "TenTo");
            ViewBag.Bp = new SelectList(tblBP, "MaBp", "TenBp");
            ViewBag.Display = display;
            return View();
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/leadload")]
        [HttpPost]
        public async Task<IActionResult> LeadLoad(int? draw, int start, string search, string maBp = "", string maTo = "", int length = 10)
        {
            if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
            {
                search = Request.Form["search[value]"];
            }

            string oderId = Request.Form["order[0][column]"];
            string oderDir = Request.Form["order[0][dir]"];

            if (draw == null)
            {
                start = (start - 1) * length;
            }

            (IEnumerable<ViewDiaryModel> data, int totals, int filter) = await _diaryRepository.GetTable(length, start, search, oderId, oderDir, maBp, maTo);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/loadto/{id=null}")]
        [HttpGet]
        public async Task<IActionResult> LoadTo(string id)
        {
            var data = await _toRepository.GetAll();
            if (id != "null")
            {
                data = data.Where(m => m.MaBp == id);
            }

            return Ok(data);
        }

        #endregion User evaluate
    }
}