#pragma checksum "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c1ab18f76f92aaae3689604ef1d574a83848ddcb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Diaries_Views_Evaluates_UserIndex), @"mvc.1.0.view", @"/Areas/Diaries/Views/Evaluates/UserIndex.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Diaries/Views/Evaluates/UserIndex.cshtml", typeof(AspNetCore.Areas_Diaries_Views_Evaluates_UserIndex))]
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
#line 1 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\_ViewImports.cshtml"
using WebCoreApp;

#line default
#line hidden
#line 2 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\_ViewImports.cshtml"
using WebCoreApp.Models;

#line default
#line hidden
#line 3 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\_ViewImports.cshtml"
using DataContext.WebCoreApp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1ab18f76f92aaae3689604ef1d574a83848ddcb", @"/Areas/Diaries/Views/Evaluates/UserIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43107aaf90a755b8fdd40040b5dcbebf71bee800", @"/Areas/Diaries/Views/_ViewImports.cshtml")]
    public class Areas_Diaries_Views_Evaluates_UserIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebCoreApp.Infrastructure.ViewModels.Diary.ViewDiaryModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/Diary/evaluate.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
  
    ViewData["Title"] = "Chi tiết nhật ký";
   

#line default
#line hidden
            DefineSection("Scripts", async() => {
                BeginContext(144, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(150, 72, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d1bc4ab872674186acefccd65787e1cb", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 7 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(222, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(227, 801, true);
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_title"">
                    <h2>Ghi nhật ký</h2>
                    <ul class=""nav navbar-right panel_toolbox"">
                        <li>
                            <a class=""collapse-link""><i class=""fa fa-chevron-up""></i></a>
                        <li>
                            <a class=""close-link""><i class=""fa fa-close""></i></a>
                        </li>
                    </ul>
                    <div class=""clearfix""></div>
                </div>
                <div class=""x_content"">
                    <!-- Title row -->
                    <div class=""row"">
                        <input type=""hidden"" id=""daily_id""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1028, "\"", 1045, 1);
#line 26 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
WriteAttributeValue("", 1036, Model.Id, 1036, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1046, 490, true);
            WriteLiteral(@" />

                        <div class=""col-xs-12 col-md-12 col-lg-12 table"">
                            <table style=""width:100%"">
                                <thead>
                                    <tr>
                                        <th style=""text-align:center;font-size:20px"">Nhật Ký Công Việc</th>
                                    </tr>
                                    <tr>
                                        <td style=""text-align:center"">Ngày ");
            EndContext();
            BeginContext(1537, 33, false);
#line 35 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
                                                                      Write(Model.Date.ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1570, 230, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n                                </thead>\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td>Họ và tên: ");
            EndContext();
            BeginContext(1801, 14, false);
#line 40 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
                                                  Write(Model.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(1815, 147, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Chức danh: ");
            EndContext();
            BeginContext(1963, 11, false);
#line 43 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
                                                  Write(Model.TenCV);

#line default
#line hidden
            EndContext();
            BeginContext(1974, 145, true);
            WriteLiteral("</td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Tổ/Nhóm: ");
            EndContext();
            BeginContext(2120, 11, false);
#line 46 "E:\2.Programming\Web PTSC Core\WebCoreApp\WebCoreApp\Areas\Diaries\Views\Evaluates\UserIndex.cshtml"
                                                Write(Model.TenTo);

#line default
#line hidden
            EndContext();
            BeginContext(2131, 2899, true);
            WriteLiteral(@"</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div><!-- /.table-responsive -->
                        <br />
                        <div class=""form-group"">
                            <button class=""btn btn-primary"" onclick=""detail(0)"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Thêm""><i class=""fa fa-plus-square-o""></i></button>
                            <button class=""btn btn-warning"" onclick=""getdata()"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Sửa""><i class=""fa fa-pencil-square-o""></i></button>
                            <button class=""btn btn-danger"" onclick=""confirmDelete()"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Xóa""><i class=""fa fa-trash-o""></i></button>
                            <button class=""btn btn-info"" onclick=""sendnotification()"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Gửi""><i class=""fa fa-envelope-o""></i></button>

          ");
            WriteLiteral(@"              </div>
                    </div>
                    <!-- Table row -->
                    <div class=""row"">
                        <div class=""col-xs-12"">
                            <table class=""table table-bordered table-condensed"" id=""dailytbl"" style=""font-family:'Times New Roman', Times, serif;font-size:11px;width:100%;color:#080808"">
                                <thead>
                                    <tr>
                                        <th colspan=""2"" ;width:10%"">Thời gian</th>
                                        <th style=""text-align:center"" rowspan=""2"">Nội dung công việc</th>
                                        <th style=""text-align:center"" rowspan=""2"">Phương pháp, công nghệ thực hiện công việc</th>
                                        <th style=""text-align:center"" rowspan=""2"">Kết quả thực hiện công việc</th>
                                        <th style=""text-align:center"" rowspan=""2"">Tổng Kết Các Công Việc Chính</th>
                    ");
            WriteLiteral(@"                    <th style=""text-align:center"" rowspan=""2"">Đánh giá của Tổ trưởng</th>
                                        <th style=""text-align:center"" rowspan=""2"">Đánh giá của Phó phòng trực tiếp</th>
                                        <th style=""text-align:center"" rowspan=""2"">Đánh giá của Trưởng Phòng</th>
                                    </tr>
                                    <tr>
                                        <th>Từ</th>
                                        <th>Đến</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div><!-- /.table-responsive -->
                    </div>
                </div>
            </div>
        </div>
    </div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebCoreApp.Infrastructure.ViewModels.Diary.ViewDiaryModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
