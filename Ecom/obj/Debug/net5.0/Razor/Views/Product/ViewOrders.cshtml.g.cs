#pragma checksum "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "731e7fda9100566fe7492cd5f2db1511c864d917"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_ViewOrders), @"mvc.1.0.view", @"/Views/Product/ViewOrders.cshtml")]
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
#line 1 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\_ViewImports.cshtml"
using Ecom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\_ViewImports.cshtml"
using Ecom.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"731e7fda9100566fe7492cd5f2db1511c864d917", @"/Views/Product/ViewOrders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4821c05eaf6eb340aa43bf49ef175b1eb1ed8737", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_ViewOrders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Models.Database.Entities.Orders>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
   ViewData["Title"] = "Products"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }

    td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 8px;
    }

    tr:nth-child(even) {
        background-color: #dddddd;
    }

    .table thead th {
        width: 10%;
    }
    .btn-info {
        color: #fff;
        background-color: #17a2b8;
        border-color: #17a2b8;
        width: 100%;
        margin-bottom: 20px;
        text-decoration: underline
    }
</style>
<div class=""container"">
    <h2>Placed Orders</h2>
");
#nullable restore
#line 35 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
      
        if (Model.ToList().Count < 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\n                <div class=\"col-lg-12\">\n                    <span class=\"text-warning text-center\">No Order Yet</span>\n                </div>\n            </div>\n");
#nullable restore
#line 43 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
        }
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-lg-12\">\n    <a");
            BeginWriteAttribute("href", " href=\"", 1024, "\"", 1050, 2);
            WriteAttributeValue("", 1031, "#demo-", 1031, 6, true);
#nullable restore
#line 48 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
WriteAttributeValue("", 1037, item.orderID, 1037, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-info\" data-toggle=\"collapse\">Order # ");
#nullable restore
#line 48 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                                                                                 Write(item.orderID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\n    <div");
            BeginWriteAttribute("id", " id=\"", 1130, "\"", 1153, 2);
            WriteAttributeValue("", 1135, "demo-", 1135, 5, true);
#nullable restore
#line 49 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
WriteAttributeValue("", 1140, item.orderID, 1140, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""collapse"">
        <div class=""table-responsive"">
            <table class=""table table-responsive table-condensed table-hover"">
                <thead>
                    <tr>
                        <th scope=""col"">Product Name</th>
                        <th scope=""col"">Product Price</th>
                        <th scope=""col"">Product Qty</th>
                    </tr>
                </thead>
                <tbody>

");
#nullable restore
#line 61 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                     foreach (var item1 in item.OrderDetails)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <th scope=\"row\">");
#nullable restore
#line 64 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                                       Write(item1.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                            <td>");
#nullable restore
#line 65 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                           Write(item1.ProductPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 66 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                           Write(item1.qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n");
#nullable restore
#line 68 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n    </div>\n</div>\n");
#nullable restore
#line 74 "C:\Users\9227\Source\Repos\super-lamp\Ecom\Views\Product\ViewOrders.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</div>\n\n\n<script>\n    $(document).ready(function ()=> {\n\n    });\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Models.Database.Entities.Orders>> Html { get; private set; }
    }
}
#pragma warning restore 1591
