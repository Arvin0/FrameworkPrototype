using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SYN.ApiCore.Extensions.RoutePrefix
{
    public static class RoutePrefixExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeTemplate)
        {
            options.Conventions.Insert(0, new RouteConvention(routeTemplate));
        }
    }
}
