using System;

namespace WebCoreApp.Data
{
    public partial class TblNotifications
    {
        public long Id { get; set; }
        public string SendId { get; set; }
        public string ReceiveId { get; set; }
        public string Url { get; set; }
        public string Notification { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}