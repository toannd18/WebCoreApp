using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.ViewModels.Notification
{
    public class ViewNotificationModel
    {
        public long Id { get; set; }
        public string SendId { get; set; }
        public string ReceiveId { get; set; }
        public string NameSend { get; set; }
        public string NameReceiveId { get; set; }
        public string Url { get; set; }
        public string Notification { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
