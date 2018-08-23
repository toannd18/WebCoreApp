using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Data
{
    public partial class TblDaily
    {
        public TblDaily()
        {
            TblDailyDetail = new HashSet<TblDailyDetail>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập ngày")]
        [DataType(DataType.Date,ErrorMessage ="Yêu cầu nhập ngày tháng")]
        public DateTime Date { get; set; }

        public string TotalJob { get; set; }
        public string Comment1 { get; set; }
        public string UserAutho1 { get; set; }
        public string UserAutho2 { get; set; }
        public bool StatusAutho1 { get; set; }
        public bool StatusAutho2 { get; set; }
        public string UserAutho3 { get; set; }
        public bool StatusAutho3 { get; set; }
        public string Comment2 { get; set; }

        public ICollection<TblDailyDetail> TblDailyDetail { get; set; }
    }
}