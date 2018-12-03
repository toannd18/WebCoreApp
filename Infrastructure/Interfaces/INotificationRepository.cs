using DataContext.WebCoreApp;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.ViewModels.Notification;

namespace WebCoreApp.Infrastructure.Interfaces
{
    public interface INotificationRepository : IGeneric<TblNotifications, long>
    {
        Task<(IEnumerable<ViewNotificationModel>, int TotalRecords)> GetNotification(string username);

        Task<bool> SaveList(List<TblNotifications> model);
    }
}