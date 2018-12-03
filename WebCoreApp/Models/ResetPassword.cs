using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Models
{
    public class ResetPassword
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Yêu cầu tài khoản")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yêu cầu địa chỉ Email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Yêu cầu địa chỉ email" )]
        public string Email { get; set; }
    }
}
