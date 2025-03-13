using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using GS.Web.Framework.Mvc.Routing;

namespace GS.WebApi.Infrastructure
{
    public class ApiUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //and default one
            //routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");

            // Controller Only
            routeBuilder.MapRoute(
                name: "ControllerOnly",
                template: "api/{controller}"
            );

            // Controller with ID
            routeBuilder.MapRoute(
                name: "ControllerAndId",
                template: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers
            );

            // Controller with actions
            routeBuilder.MapRoute(
                name: "ControllerAndAction",
                template: "api/{controller}/{action}"
            );

            // Controller with actions
            routeBuilder.MapRoute(
                name: "ControllerAndActionAndId",
                template: "api/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers
            );
           
            
        }

        #region Properties

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority
        {
            //it should be the last route. we do not set it to -int.MaxValue so it could be overridden (if required)
            get { return -1000000; }
        }

        #endregion
    }
}
