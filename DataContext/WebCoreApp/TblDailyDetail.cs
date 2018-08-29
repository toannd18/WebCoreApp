using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class TblDailyDetail
    {
        public long Id { get; set; }
        public TimeSpan FormTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string Comment1 { get; set; }
        public string ContentJob { get; set; }
        public string Method { get; set; }
        public string Result { get; set; }
        public int DailyId { get; set; }
        public int JobId { get; set; }
        public int? Level1 { get; set; }
        public int? Level2 { get; set; }
        public string Comment2 { get; set; }
        public int? Level3 { get; set; }
        public string Comment3 { get; set; }

        public TblDaily Daily { get; set; }
    }
}
