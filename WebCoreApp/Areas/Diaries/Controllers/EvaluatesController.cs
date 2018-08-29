using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Areas.Diaries.Models;
using WebCoreApp.Constants;
using DataContext.WebCoreApp;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Areas.Diaries.Controllers
{
    [Area("Diaries")]
    public class EvaluatesController : Controller
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IDetailDiaryRepository _detailDiaryRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IBPRepository _bPRepository;

        public EvaluatesController(IDiaryRepository diaryRepository,
            IDetailDiaryRepository detailDiaryRepository,
            IJobRepository jobRepository,
            IBPRepository bPRepository)
        {
            _detailDiaryRepository = detailDiaryRepository;
            _diaryRepository = diaryRepository;
            _jobRepository = jobRepository;
            _bPRepository = bPRepository;
        }

        #region Nhân viên tao báo cáo

        [Route("[area]/[controller]/UserIndex/{Id}")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> UserIndex(int Id)
        {
            var tbl = await _diaryRepository.GetViewByMa(Id);
            return View(tbl);
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/load")]
        [HttpPost]
        public async Task<IActionResult> Load(int id, int? draw, int start, string search, int length = 5)
        {
            if (!string.IsNullOrEmpty(Request.Form["search[value]"]))
            {
                search = Request.Form["search[value]"];
            }
            search = Request.Form["search[value]"];
            string oderId = Request.Form["order[0][column]"];
            string oderDir = Request.Form["order[0][dir]"];

            if (draw == null)
            {
                start = (start - 1) * length;
            }

            (IEnumerable<ViewDetailDiaryModel> data, int totals, int filter) = await _detailDiaryRepository.GetTable(length, start, search, oderId, oderDir, id);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Route("[area]/[controller]/userget/{Id}")]
        [HttpGet]
        public async Task<IActionResult> UserGet(long Id)
        {
            string to = HttpContext.User.FindFirst("Ma_To").Value;
            string bp = HttpContext.User.FindFirst("Ma_BP").Value;
            TblDailyDetail tbl = new TblDailyDetail();
            if (Id > 0)
            {
                tbl = await _detailDiaryRepository.GetByMa(Id);
            }
            ViewBag.JobList = new SelectList(await _jobRepository.GetJobList(to, bp), "Id", "TenJob");
            return PartialView("_UserInput", tbl);
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/savediary")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] TblDailyDetail tbl)
        {
            bool status = await (tbl.Id > 0 ? _detailDiaryRepository.Update(tbl) : _detailDiaryRepository.Save(tbl));
            return Ok(new { status = status });
        }

        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [Route("[area]/api/[controller]/delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            bool status = await _detailDiaryRepository.Delete(id);
            return Ok(new { status = status });
        }

        #endregion Nhân viên tao báo cáo

        #region Đánh giá

        [Route("[area]/[controller]/Index/{Id}")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Display = int.Parse(HttpContext.User.FindFirst("Display").Value);
            var tbl = await _diaryRepository.GetViewByMa(Id);
            return View(tbl);
        }

        [Route("[area]/[controller]/Comment/{Id}")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Comment(long Id)
        {
            int Display = int.Parse(HttpContext.User.FindFirst("Display").Value);
            ViewCommentModel model = new ViewCommentModel();
            var tbl = await _detailDiaryRepository.GetByMa(Id);
            var job = await _jobRepository.GetAll();

            ViewBag.JobList = new SelectList(job.Where(m=>m.Id==tbl.JobId), "Id", "TenJob");
            model.id = tbl.Id;
            model.jobId = tbl.JobId;
            model.method = tbl.Method;
            model.contentJob = tbl.ContentJob;
            model.result = tbl.Result;
            if (Display == 3)
            {
                model.level = tbl.Level1.HasValue ? tbl.Level1.Value : 3;
                model.comment = tbl.Comment1;
            }
            else if (Display == 4)
            {
                model.level = tbl.Level2.HasValue ? tbl.Level2.Value : 3;
                model.comment = tbl.Comment2;
            }
            else if (Display > 4)
            {
                model.level = tbl.Level3.HasValue ? tbl.Level3.Value : 3;
                model.comment = tbl.Comment3;
            }
            return PartialView("_Comment",model);
        }
        [Route("[area]/api/[controller]/savecomment")]
        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> SaveComment([FromBody] ViewCommentModel tbl)
        {
            int Display = int.Parse(HttpContext.User.FindFirst("Display").Value);
            bool status;
            TblDailyDetail model;
            model = await _detailDiaryRepository.GetByMa(tbl.id);
            if (Display == 3)
            {
                
                model.Comment1 = tbl.comment;
                model.Level1 = tbl.level;
            }
            else if (Display == 4)
            {
                model.Comment2 = tbl.comment;
                model.Level2 = tbl.level;
            }
            else if(Display > 4)
            {
                model.Comment3 = tbl.comment;
                model.Level3 = tbl.level;
            }
            status = await _detailDiaryRepository.Update(model);
            return Ok(new { status = status });
        }

        [Route("[area]/api/[controller]/savecommentall")]
        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> SaveCommentAll([FromBody] ViewCommentModel tbl)
        {
            int Display = int.Parse(HttpContext.User.FindFirst("Display").Value);
            bool status;
            
        
            status = await _detailDiaryRepository.SaveCommentAll(tbl,Display);
            return Ok(new { status = status });
        }
        #endregion Đánh giá

        [Route("[area]/api/[controller]/SendNotification/{id}")]
        [Authorize(AuthenticationSchemes = CommonConstants.AuthSchemes)]
        [HttpPost]
        public async Task<IActionResult> SendNotification(int id)
        {
           bool status= await _diaryRepository.SendDiary(id);
            return Ok(new { status = status });
        }
    }
}