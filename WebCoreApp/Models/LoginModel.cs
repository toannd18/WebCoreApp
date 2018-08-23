using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Yêu cầu nhập tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Yêu cầu nhập mật khẩu",AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public bool Remember { get; set; }
    }
}
