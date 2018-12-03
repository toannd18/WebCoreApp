using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp
{
    public partial class Functions
    {
        public Functions()
        {
            InverseParent = new HashSet<Functions>();
            Permissions = new HashSet<Permissions>();
        }
        [Required(ErrorMessage ="Yêu cầu nhập mã")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập đường dẫn")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập sắp xếp")]
        public int DisplayOrder { get; set; }
        public string ParentId { get; set; }
        public bool? Status { get; set; }
        public string IconCss { get; set; }

        public Functions Parent { get; set; }
        public ICollection<Functions> InverseParent { get; set; }
        public ICollection<Permissions> Permissions { get; set; }
    }
}
