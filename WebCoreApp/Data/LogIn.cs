using System;

namespace WebCoreApp.Data
{
    public partial class LogIn
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public long IdName { get; set; }
        public string TblName { get; set; }
    }
}