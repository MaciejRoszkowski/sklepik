using sklepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sklepik.Controllers
{
    public class HomeController : Controller
    {
        private ShopDB _db = new ShopDB();
        public ActionResult Index()
        {
            return View(_db.Product
                .Where(p=>p.IsHidden==false)
                .ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}