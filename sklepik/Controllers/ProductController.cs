using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sklepik.Models;

namespace sklepik.Controllers
{

    public class ProductController : Controller
    {
        private ShopDB _db = new ShopDB();

        // GET: Product
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            return View(_db.Product.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Product/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase image1, HttpPostedFileBase image2)
        {
            if(ModelState.IsValid)
            {
                if(image1 !=null)
                {
                    product.Image =new byte[image1.ContentLength];
                    image1.InputStream.Read(product.Image,0,image1.ContentLength);

                }
                if(image2!=null)
                {
                    product.ImageSmall = new byte[image2.ContentLength];
                    image2.InputStream.Read(product.ImageSmall, 0, image2.ContentLength);
                }
                _db.Product.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _db.Product.Find(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image1, HttpPostedFileBase image2)
        {
            if(ModelState.IsValid)
            {
                //var product1 = _db.Entry(product).OriginalValues;

                if (image1 != null)
                {
                    
                    product.Image = new byte[image1.ContentLength];
                    image1.InputStream.Read(product.Image, 0, image1.ContentLength);

                }
                if (image2 != null)
                {
                    product.ImageSmall = new byte[image2.ContentLength];
                    image2.InputStream.Read(product.ImageSmall, 0, image2.ContentLength);
                }

                _db.Entry(product).State=EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Product.Find(id);
            if(product==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = _db.Product.Find(id);
                _db.Product.Remove(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
