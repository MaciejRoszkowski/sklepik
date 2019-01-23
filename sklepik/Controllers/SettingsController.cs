using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace sklepik.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeLanguage(string LanguageAbbrevation)
        {
            if(LanguageAbbrevation !=null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture =new CultureInfo(LanguageAbbrevation);

            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return View("Index");
        }
        [HttpPost]
        public ActionResult SelectOptions(int vat,int theme, string currency, int pages)
        {
            HttpCookie cookieVat = new HttpCookie("Vat");
            cookieVat.Value = vat.ToString();
            Response.Cookies.Add(cookieVat);


            HttpCookie cookieTheme = new HttpCookie("Theme");
            cookieTheme.Value = theme.ToString();
            Response.Cookies.Add(cookieTheme);

            HttpCookie cookieCurrency = new HttpCookie("Currency");
            cookieCurrency.Value = currency;
            Response.Cookies.Add(cookieCurrency);

            HttpCookie cookiePages = new HttpCookie("Pages");
            cookiePages.Value = pages.ToString();
            Response.Cookies.Add(cookiePages);

            return View("Index");

        }
    }
}