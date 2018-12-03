using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Controllers.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IFunctionRepository functionRepository;

        public MenuViewComponent(IFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Functions> model = await functionRepository.GetAll();
            return View("_Menu", model);
        }
    }
}