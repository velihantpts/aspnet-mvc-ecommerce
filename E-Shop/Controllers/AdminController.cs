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
    public class AdminController : Controller
    {
        // GET: Admin
        Context1 db = new Context1();
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CommentList(int sayfa=1)
        {
            return View(db.Comments.ToList().ToPagedList(sayfa,3));
        }

        public ActionResult CommentDelete(int id)
        {
            var delete = db.Comments.Where(x => x.Id == id).FirstOrDefault();
            db.Comments.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("CommentList");
        }
    }
}