using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class WelderViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Yêu cầu nhập mã hiệu")]
        [StringLength(20,ErrorMessage ="Không được vượt quá 20 ký tự")]
        [DisplayName("Mã hiệu")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập tên")]
        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        [DisplayName("Họ và tên")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Yêu cầu nhập ngày tháng")]
        [DataType(DataType.Date,ErrorMessage ="Yều nhập định dạng ngày tháng")]
        [DisplayName("Ngày sinh")]
        public DateTime BrithDay { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [NotMapped]
        public bool Update { get; set; }

        public WelderViewModel()
        {
            Update = false;
            Status = true;
            BrithDay = DateTime.Today;
        }
    }
}
