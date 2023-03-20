using DataAccessLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace EShop.Controllers
{
    public class AdminSalesController : Controller
    {
        // GET: AdminSales
        Context1 db = new Context1();
        public ActionResult Index(int sayfa=1)
        {
            return View(db.Sales.ToList().ToPagedList(sayfa, 5));
        }
    }
}