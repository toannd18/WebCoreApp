using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Areas.Pipes.Models
{
    public class JointViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập mối")]
   
        public string Joint { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Rev")]
        public string Rev { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập kiểu mối")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string TypeJoint { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập kiểu mối")]
        [StringLength(1, ErrorMessage = "Không được vượt quá 1 ký tự")]
        public string SF { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Size")]
        [RegularExpression(@"^\d+\.?\d{0,2}?$", ErrorMessage = "Số không hợp lệ")]
        public decimal Size { get; set; }

        public string Status { get; set; }

        public string IsoName { get; set; }

        public bool UpdateJoint { get; set; }
    }
}