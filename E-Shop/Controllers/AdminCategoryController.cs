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

    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        // GET: AdminKategori

        CategoryRepository categoryRepository = new CategoryRepository();
        Context1 db = new Context1();
        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }
        public ActionResult Create()
        {
            return View();

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Category data)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(data);
                return RedirectToAction("Index");
            }
            return View(data);
           

        }

        public ActionResult Update(int id)
        {
            var cat = categoryRepository.GetById(id);
            return View(cat);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Category data)
        {
            if (ModelState.IsValid)
            {
                var cat = categoryRepository.GetById(data.Id);
                cat.Description = data.Description;
                cat.Name = data.Name;
                categoryRepository.Update(cat);
                return RedirectToAction("Index");
            }
            return View(data);
            
        }

        public ActionResult Delete(int id)
        {
            var cat = categoryRepository.GetById(id);
            categoryRepository.Delete(cat);

            return RedirectToAction("Index");
        }

       


    }
}