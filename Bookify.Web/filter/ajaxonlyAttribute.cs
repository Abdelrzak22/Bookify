using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Bookify.Web.filter
{
    public class ajaxonlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var request=routeContext.HttpContext.Request;
            var isAjax = request.Headers["x-requested-with"] == "XMLHttpRequest";
            return isAjax;
        }
    }
}
