using Umbraco.SlashBase.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RouteConfig), "RegisterRoutes")]

namespace Umbraco.SlashBase.App_Start
{
    using System.Web.Http;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration bootstrapper
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        public static void RegisterRoutes()
        {
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultBase",
                routeTemplate: "uBase/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}