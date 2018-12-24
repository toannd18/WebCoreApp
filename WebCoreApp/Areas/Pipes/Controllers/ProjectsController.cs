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
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _map;

        public ProjectsController(IProjectRepository projectRepository, IMapper map)
        {
            _projectRepository = projectRepository;
            _map = map;
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

            (IEnumerable<Project> data, int totals, int filter) = await _projectRepository.GetTable(record, start, search, orderId, orderDir);

            return Ok(new { draw = draw, recordsTotal = totals, recordsFiltered = filter, data = data });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, bool update)
        {
            ProjectViewModel model = new ProjectViewModel();
            model.Update = update;
            if (update)
            {
                var tbl = await _projectRepository.GetByMa(id);
                _map.Map<Project, ProjectViewModel>(tbl, model);
            }
            return PartialView("_ProjectView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]  ProjectViewModel model)
        {
            Project tbl = new Project();
            string user = HttpContext.User.Identity.Name;
            
            bool status;
            if (model.Update)
            {
                tbl = await _projectRepository.GetByMa(model.Id);
                _map.Map<ProjectViewModel, Project>(model, tbl);
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
                status = await _projectRepository.Update(tbl);
                if (status)
                {
                    return Ok("Cập nhật thành công");
                }
                return BadRequest("Cập nhật thất bại");
            }
            _map.Map<ProjectViewModel, Project>(model, tbl);
            tbl.UserCreated = user;
            tbl.DateCreated = DateTime.Now;
            status = await _projectRepository.Save(tbl);
            if (status)
            {
                return Ok("Thêm thành công");
            }
            return BadRequest("Thêm thất bại");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool status = await _projectRepository.Delete(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");
        }
    }
}