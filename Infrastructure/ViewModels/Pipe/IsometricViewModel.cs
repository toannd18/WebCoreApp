using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class IsometricViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập tên bản vẽ")]
        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        [Remote(controller: "Validates", action: "ExistDraw", AdditionalFields = nameof(Project))]
        public string DrawName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Rev")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string Rev { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Unit")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string Unit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập loại ống")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string PipeClass { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập line")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string Line { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập Size")]
        [RegularExpression(@"^\d+\.?\d{0,2}?$", ErrorMessage = "Số không hợp lệ")]
        public decimal Size { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập kiểu mối")]
        [StringLength(4, ErrorMessage = "Không được vượt quá 4 ký tự")]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập loại vật liệu")]
        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        public string Material { get; set; }

        [StringLength(255, ErrorMessage = "Bạn nhập quá dài")]
        public string Remark { get; set; }

        public bool Update { get; set; }

        public Guid Project { get; set; }

        public IsometricViewModel()
        {
            Rev = "0";
            Size = 0;
        }
    }
}