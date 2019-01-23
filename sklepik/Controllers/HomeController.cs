using sklepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace sklepik.Controllers
{
    public class HomeController : Controller
    {
        private ShopDB _db = new ShopDB();
        public ActionResult Index(string searchTerm =null,int page=1)
        {
            return View(_db.Product
                .Where(p=>p.IsHidden==false&&(searchTerm==null||p.Name.StartsWith(searchTerm)))
                .OrderBy(p=>p.Id)
                .ToPagedList(page, Int32.Parse(Request.Cookies["Pages"].Value)));
        }
        //Int32.Parse(Request.Cookies["Pages"].Value)
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