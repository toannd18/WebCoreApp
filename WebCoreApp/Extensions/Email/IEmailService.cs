using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Extensions.Email
{
   public interface IEmailService
    {
        Task SendAsync(string subject, string message, string mailTo, string mailCc = null, bool isHtml =true);
    }
}
