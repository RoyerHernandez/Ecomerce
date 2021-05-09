using Ecomerce.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ecomerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.EcomerceContext, Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            CheckRolesAndSuperUser();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
              = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        private void CheckRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Admin");
            UsersHelper.CheckRole("User");
            UsersHelper.CheckRole("Customer");
            UsersHelper.CheckRole("Supplier");
            UsersHelper.CheckSuperUser();
        }
    }
}
