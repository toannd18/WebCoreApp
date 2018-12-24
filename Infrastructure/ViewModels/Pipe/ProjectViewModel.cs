using System;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập mã dự án")]
        [Display(Name = "Mã dự án")]
        [StringLength(5, ErrorMessage = "Không được vượt quá năm ký tự")]
        [RegularExpression("[a-zA-Z0-9]+", ErrorMessage = "Không cho phép nhập ký tự UNICODE")]
        public string Name { get; set; }

        [Display(Name = "Diễn giải")]
        [StringLength(255, ErrorMessage = "Bạn nhập quá dài")]
        public string Description { get; set; }

        public bool Update { get; set; }

    }
}