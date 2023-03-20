using BusinessLayer.Concrete.Repositories;
using DataAccessLayer.DataContext;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductRepository productRepository = new ProductRepository();
        Context1 db = new Context1();
        public ActionResult Details(int id)
        {
            var details = productRepository.GetById(id);
            var yorum = db.Comments.Where(x => x.ProductId == id).ToList();
            ViewBag.yorum = yorum;
          
            return View(details);
        }
        public PartialViewResult PopularProduct()
        {
            var product = productRepository.GetPopularProduct();
            ViewBag.popular = product;
            return PartialView();
        }

        //public JsonResult Comment(string icerik,int productid,string username )
        //{
          
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (icerik==null)
        //        {
        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        db.Comments.Add(new Comment { Icerik = icerik, ProductId = productid,Username=username});
        //        db.SaveChanges();
        //    }
            
        //    return Json(false, JsonRequestBehavior.AllowGet);
        //}
        
        [HttpPost]
        public ActionResult Comment(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
                db.Comments.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Login", "Account");
          
        }

        
       [HttpGet]
       public ActionResult CommentGet(int id)
        {
            var getupdate = db.Comments.Where(x => x.Id == id).FirstOrDefault();
           
            return View(getupdate);
        }
        

        [HttpPost]
        public ActionResult CommentGet(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
               
                var update = db.Comments.Where(x => x.Id == data.Id).FirstOrDefault();
                update.Icerik = data.Icerik;
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }
           
            return RedirectToAction("Login", "Account");
            
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CouponDiscount(int ProductCouponCode)
        {
            var code = db.Products.Where(x => x.ProductCouponCode == ProductCouponCode).FirstOrDefault();

            if (User.Identity.IsAuthenticated)
            {
                if (Session["Mail"]!=null)
                {
                    if (code.ProductCouponCode == ProductCouponCode)
                    {
                        code.Price = code.Price - 50;
                        db.SaveChanges();
                    }
                }
                else
                {
                    return View();
                }
                
            }
          
            
            

           
            return View();
        }



    }
}