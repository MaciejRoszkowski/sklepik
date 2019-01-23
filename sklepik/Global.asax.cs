using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace sklepik
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["Totaluser"] = 0;
            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            HttpCookie cookieCurrency = HttpContext.Current.Request.Cookies["Currency"];
            HttpCookie cookiePages = HttpContext.Current.Request.Cookies["Pages"];
            HttpCookie cookieTheme = HttpContext.Current.Request.Cookies["Theme"];
            HttpCookie cookieVat = HttpContext.Current.Request.Cookies["Vat"];



            //if (cookieCurrency == null||cookieCu.Value!=null)



            if (cookie != null&&cookie.Value!=null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
            //if (cookieCurrency == null || cookieCurrency.Value != null)
            //{
            //    cookieCurrency.Value = "pln";
            //    Response.Cookies.Add(cookieCurrency);

            //}
            //if (cookiePages == null || cookiePages.Value != null)
            //{
            //    cookiePages.Value = "5";
            //    Response.Cookies.Add(cookiePages);

            //}
            //if (cookieTheme == null || cookieTheme.Value != null)
            //{
            //    cookieTheme.Value = "0";
            //    Response.Cookies.Add(cookieTheme);

            //}
            //if (cookieVat == null || cookieVat.Value != null)
            //{
            //    cookieVat.Value = "0";
            //    Response.Cookies.Add(cookieVat);

            //}

        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["Totaluser"] = (int)Application["Totaluser"] + 1;
            Application.UnLock();
        }
    }
}
