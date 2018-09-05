using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;

namespace SYN.ApiCore.Extensions.RoutePrefix
{
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RouteConvention(IRouteTemplateProvider routeTemplate)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplate);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var routeSelectors = controller.Selectors.Where(s => s.AttributeRouteModel != null).ToList();
                if (routeSelectors.Any())
                {
                    foreach (var selector in routeSelectors)
                    {
                        selector.AttributeRouteModel =
                            AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                                selector.AttributeRouteModel);

                    }
                }

                var unRouteSelectors = controller.Selectors.Where(s => s.AttributeRouteModel == null).ToList();
                if (unRouteSelectors.Any())
                {
                    foreach (var selector in unRouteSelectors)
                    {
                        selector.AttributeRouteModel = _centralPrefix;
                    }
                }
            }
        }
    }
}
