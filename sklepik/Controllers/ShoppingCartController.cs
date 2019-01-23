using sklepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sklepik.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShopDB _db = new ShopDB();
        // GET: ShoppingCart
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult OrderNow(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if(Session["Cart"]==null)
            {
                List<Item> lsCart = new List<Item>
                {
                    new Item(_db.Product.Find(id),1)
                };
                Session["Cart"] = lsCart;
            }
            else
            {
                
                List<Item> cart = (List<Item>)Session["Cart"];
                int check = IsExistingCheck(id);
                if (check ==-1)
                    cart.Add(new Item(_db.Product.Find(id), 1));
                else
                    cart[check].Quantity++;
                

                Session["Cart"] = cart;
            }
            return View("Index");
        }
        private int IsExistingCheck(int? id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                    return i;
            }
            return -1;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            int check = IsExistingCheck(id);
            List<Item> cart = (List<Item>)Session["Cart"];
            //TODO adding to trash 
            if(cart[check]!=null)
                cart.RemoveAt(check);
            Session["Cart"] = cart;


            return View("Index");
        }
        
    }
}