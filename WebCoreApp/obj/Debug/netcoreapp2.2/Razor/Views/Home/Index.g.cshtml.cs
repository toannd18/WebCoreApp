#pragma checksum "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6a5170ab35bba467ab263a9c9784d8a8c3e5bb71"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\_ViewImports.cshtml"
using WebCoreApp.Models;

#line default
#line hidden
#line 2 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\_ViewImports.cshtml"
using DataContext.WebCoreApp;

#line default
#line hidden
#line 3 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\_ViewImports.cshtml"
using WebCoreApp.Infrastructure.ViewModels.Notification;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6a5170ab35bba467ab263a9c9784d8a8c3e5bb71", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ba1ded73af13f8c8ea1ca7ec22a28aa563f7919", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml"
  

    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
    var claims = User.Claims.Select(c => new { type=c.Type, value=c.Value }).ToList();

#line default
#line hidden
            BeginContext(149, 77, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <table class=\"table tab-content\">\r\n        <tbody>\r\n");
            EndContext();
#line 10 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml"
             foreach (var item in claims)
            {


#line default
#line hidden
            BeginContext(286, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(333, 9, false);
#line 14 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml"
                   Write(item.type);

#line default
#line hidden
            EndContext();
            BeginContext(342, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(374, 10, false);
#line 15 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml"
                   Write(item.value);

#line default
#line hidden
            EndContext();
            BeginContext(384, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 17 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Views\Home\Index.cshtml"


            }

#line default
#line hidden
            BeginContext(433, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            EndContext();
            BeginContext(7129, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
