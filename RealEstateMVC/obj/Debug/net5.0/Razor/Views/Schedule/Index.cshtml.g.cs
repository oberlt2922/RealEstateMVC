#pragma checksum "C:\Users\oberl\Documents\GitHub\RealEstateMVC\RealEstateMVC\Views\Schedule\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ff39711240bced3ce0a9002e98db8d2338a4cd89"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Schedule_Index), @"mvc.1.0.view", @"/Views/Schedule/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\oberl\Documents\GitHub\RealEstateMVC\RealEstateMVC\Views\_ViewImports.cshtml"
using RealEstateMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\oberl\Documents\GitHub\RealEstateMVC\RealEstateMVC\Views\_ViewImports.cshtml"
using RealEstateMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff39711240bced3ce0a9002e98db8d2338a4cd89", @"/Views/Schedule/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3727405c7eeeaca506f68dee72cb83f6bc10f780", @"/Views/_ViewImports.cshtml")]
    public class Views_Schedule_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RealEstateMVC.Models.HouseListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\oberl\Documents\GitHub\RealEstateMVC\RealEstateMVC\Views\Schedule\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_LayoutHouseList.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("h1", async() => {
                WriteLiteral("\r\n    <h1>Schedule Viewing</h1>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RealEstateMVC.Models.HouseListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591