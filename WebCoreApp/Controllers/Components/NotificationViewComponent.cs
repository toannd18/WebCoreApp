using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Models;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Controllers.Components
{
    public class NotificationViewComponent:ViewComponent
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationViewComponent(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string user = HttpContext.User.Identity.Name.ToString();
            (IEnumerable<ViewNotificationModel>data ,int TotalRecords) model = await _notificationRepository.GetNotification(user);
            ViewBag.Count = model.TotalRecords;
            return View("_Notification", model.data);
        }
    }
}
