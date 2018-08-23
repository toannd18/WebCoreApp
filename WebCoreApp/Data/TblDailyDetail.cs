using System;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Data
{
    public partial class TblDailyDetail
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu chọn thời gian")]
        public TimeSpan FormTime { get; set; }

        [Required(ErrorMessage = "Yêu cầu chọn thời gian")]
        public TimeSpan ToTime { get; set; }

        public string Comment1 { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập nội dung công việc")]
        public string ContentJob { get; set; }

        public string Method { get; set; }

        [StringLength(250, ErrorMessage = "Không được nhập quá 250 ký tự")]
        public string Result { get; set; }

        public int DailyId { get; set; }

        [Required(ErrorMessage = "Yêu cầu chọn hạng mục công việc")]
        public int JobId { get; set; }

        public int? Level1 { get; set; }
        public int? Level2 { get; set; }
        public string Comment2 { get; set; }
        public int? Level3 { get; set; }
        public string Comment3 { get; set; }

        public TblDaily Daily { get; set; }
    }
}