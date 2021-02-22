using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoogleMapsManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Commenting out the error handling code. The version on iapps didn't have this. Leaving this in
            // generates a lot of emails that aren't really errors.

            //string qualifiedUserID = System.Web.HttpContext.Current.User.Identity.Name;
            //Exception ex;
            //ex = Server.GetLastError();

            //if (!ex.ToString().ToUpper().Contains("Invalid ViewState".ToUpper()) && !ex.ToString().ToUpper().Contains("Invalid script resource or webresource request".ToUpper()))
            //{
            //    EmailHelper.SendMail(ConfigurationManager.AppSettings["ErrorLoggingEmail"],
            //                         "Error in Google Maps Manager Application. Environment: " + System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString(),
            //                         "User: " + qualifiedUserID + " Browsed the following page: " + Request.FilePath + ", and Received the following error: " + ex.ToString(),
            //                         false);
            //}
        }
    }
}
