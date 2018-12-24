using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCoreApp.Infrastructure.ViewModels.Pipe
{
    public class MaterialViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu nhập mã")]
        [StringLength(20, ErrorMessage = "Không được vượt quá 20 ký tự")]
        [DisplayName("Mã vật liệu")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        public bool Update { get; set; }
    }
}