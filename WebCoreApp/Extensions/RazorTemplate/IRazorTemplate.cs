using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Extensions.RazorTemplate
{
   public interface IRazorTemplate
    {
       Task<string> Templates(string PathName,object model,string key);
    }
}
