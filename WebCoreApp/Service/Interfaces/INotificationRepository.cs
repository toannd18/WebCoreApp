using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Data;
using WebCoreApp.Models;

namespace WebCoreApp.Service.Interfaces
{
   public interface INotificationRepository:IGeneric<TblNotifications,long>
    {
        Task<(IEnumerable<ViewNotificationModel>, int TotalRecords)> GetNotification(string username);
        Task<bool> SaveList(List<TblNotifications> model);
    }
}
