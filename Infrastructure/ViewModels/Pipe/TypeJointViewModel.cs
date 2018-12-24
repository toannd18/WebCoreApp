using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class TypeJointViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Yêu cầu nhập mã")]
        [StringLength(4,ErrorMessage ="Không được vượt quá 4 ký tự")]
        [DisplayName("Mã mối hàn")]
        public string Type { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        public bool Update { get; set; }
    }
}
