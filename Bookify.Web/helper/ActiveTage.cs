using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bookify.Web.helper
{
    [HtmlTargetElement("a", Attributes = "active-when")]
    public class ActiveTage:TagHelper
    {
        public string? ActiveWhen { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(ActiveWhen))
                return;
            var currentContext = ViewData?.RouteData.Values["controller"]?.ToString();
            if(currentContext!.Equals(ActiveWhen))
            {
                if(output.Attributes.ContainsName("class"))
                {
                    output.Attributes.SetAttribute("class", $"{output.Attributes["class"].Value} active");
                }
                else
                {
                    output.Attributes.SetAttribute("class",  "active");

                }


            }
        }

    }
}
