using BusinessLayer.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DataAccessLayer.DataContext;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ProductRepository productRepository = new ProductRepository();
        Context1 db = new Context1();
        public ActionResult Index(int sayfa=1)
        {
            var product = productRepository.GetPopularProduct();
            ViewBag.popular = product;
            return View(productRepository.GetProductList().ToPagedList(sayfa, 3));
        }

       

    }
}