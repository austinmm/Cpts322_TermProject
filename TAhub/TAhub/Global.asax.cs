using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TAhub
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
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                DB.Error error = new DB.Error();
                error.Message = exception.Message;
                string type = exception.GetType().ToString();
                if ((error.HasInnerException = exception.InnerException != null) == true)
                {
                    error.InnerExceptionMessage = exception.InnerException.Message;
                }
                try
                {
                    StackTrace trace = new StackTrace(exception, true);
                    StackFrame frame = trace.GetFrame(0);
                    error.LineNumber = frame.GetFileLineNumber();
                    error.FileName = frame.GetMethod().DeclaringType.FullName;
                    error.NameSpace = frame.GetMethod().DeclaringType.Namespace;
                    error.FilePath = frame.GetFileName();
                }
                catch (Exception) { }
                DB.DBMethods.Insert(error);
                // Clear the response stream 
                var httpContext = ((HttpApplication)sender).Context;
                httpContext.Response.Clear();
                httpContext.ClearError();
                httpContext.Response.TrySkipIisCustomErrors = true;
                // Manage to display a friendly view 
                InvokeErrorAction(httpContext, exception);
            }
        }
        private void InvokeErrorAction(HttpContext httpContext, Exception exception)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "home";
            routeData.Values["action"] = "error";
            routeData.Values["id"] = "this is a test";
            using (var controller = new Controllers.HomeController())
            {
                var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
                ((IController)controller).Execute(requestContext);
            }
        }     
    }
}
