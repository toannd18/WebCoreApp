using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class WeldingViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu chọn mối hàn")]
        public string txtjoint { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập ngày tháng")]
        public DateTime? WeldingDate { get; set; }

        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        public string Heate1 { get; set; }

        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        public string Heate2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập thợ hàn")]
        [StringLength(20, ErrorMessage = "Không được vượt quá 10 ký tự")]
        [Remote(action: "ExistWelder", controller: "Validates")]
        public string Welder1 { get; set; }

        [StringLength(20, ErrorMessage = "Không được vượt quá 10 ký tự")]
        [Remote(action: "ExistWelder", controller: "Validates")]
        public string Welder2 { get; set; }

        [StringLength(20, ErrorMessage = "Không được vượt quá 10 ký tự")]
        [Remote(action: "ExistWelder", controller: "Validates")]
        public string Welder3 { get; set; }

        [StringLength(20, ErrorMessage = "Không được vượt quá 10 ký tự")]
        [Remote(action: "ExistWelder", controller: "Validates")]
        public string Welder4 { get; set; }

        [StringLength(255, ErrorMessage = "Quá dài")]
        public string Remark { get; set; }
    }
}