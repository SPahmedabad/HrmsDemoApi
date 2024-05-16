using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Feedback_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    var application = sender as HttpApplication;
        //    if (application != null && application.Context != null)
        //    {
        //        application.Context.Response.Headers.Remove("Server");
        //    }
        //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //    {
        //        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        //        HttpContext.Current.Response.End();
        //    }
        //}
        //protected void Application_PreSendRequestHeaders()
        //{
        //    Response.Headers.Remove("Server");
        //    Response.AddHeader("X-Frame-Options", "DENY");
        //}
    }
}
