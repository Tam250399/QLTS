﻿using Microsoft.AspNetCore.Routing;

namespace GS.Web.Framework.Mvc.Routing
{
    /// <summary>
    /// Route provider
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routeBuilder">Route builder</param>
        void RegisterRoutes(IRouteBuilder routeBuilder);

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        int Priority { get; }
    }
}
