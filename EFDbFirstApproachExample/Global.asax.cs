using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace EFDbFirstApproachExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_Error()
        {
            Exception exce = Server.GetLastError();
            string s = "Message: " + exce.Message + ", Type: " + exce.GetType().ToString() + ", Source: " + exce.Source;

            StreamWriter sw = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + "\\ErrorLog.txt");
            sw.WriteLine(s);
            sw.Close();
            Response.Redirect("Error.html");
        }
    }
}
