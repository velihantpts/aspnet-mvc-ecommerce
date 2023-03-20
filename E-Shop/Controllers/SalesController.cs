using DataAccessLayer.DataContext;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;


namespace EShop.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        Context1 db = new Context1();
        public ActionResult Index(int sayfa = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.Users.FirstOrDefault(x => x.Email == kullaniciadi);
                var model = db.Sales.Where(x => x.UserId == kullanici.Id).ToList().ToPagedList(sayfa, 5);
                return View(model);
            }
            return HttpNotFound();
        }

        public ActionResult Buy(int id)
        {
            var model = db.Carts.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Buy2(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.Carts.FirstOrDefault(x => x.Id == id);

                    var satis = new Sales
                    {
                        UserId = model.UserId,
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        Image = model.Image,
                        Price = model.Price,
                        Date = model.Date,

                    };
                    db.Carts.Remove(model);
                    db.Sales.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın Alma İşlemi Başarılı Bir Şekilde Gerçekleşmiştir";
                }
            }
            catch (Exception)
            {

                ViewBag.islem = "Satın Alma İşlemi Başarısız ";
            }

            return View("islem");

        }

        public ActionResult BuyAll(decimal? Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.Users.FirstOrDefault(x => x.Email == kullaniciadi);
                var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
                var kid = db.Carts.FirstOrDefault(x => x.UserId == kullanici.Id);
                if (model != null)
                {
                    if (kid == null)
                    {
                        ViewBag.Tutar = "Sepetinizde ürün bulunmamaktadır";
                    }
                    else if (kid != null)
                    {
                        Tutar = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.Tutar = "Toplam Tutar = " + Tutar + "TL";
                    }
                    return View(model);
                }

                return View();
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult BuyAll2()
        {
            var username = User.Identity.Name;
            var kullanici = db.Users.FirstOrDefault(x => x.Email == username);
            var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
            int row = 0;
            foreach (var item in model)
            {
                var satis = new Sales
                {
                    UserId = model[row].UserId,
                    ProductId = model[row].ProductId,
                    Quantity = model[row].Quantity,
                    Price = model[row].Price,
                    Image = model[row].Product.Image,
                    Date = DateTime.Now
                };
                db.Sales.Add(satis);
                db.SaveChanges();
                row++;
            }
            db.Carts.RemoveRange(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}