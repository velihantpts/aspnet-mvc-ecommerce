using DataAccessLayer.DataContext;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;

namespace EShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        Context1 db = new Context1();
       
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(User data)
        {
            var bilgiler = db.Users.FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);
           
                if (bilgiler != null)
                {

                    FormsAuthentication.SetAuthCookie(bilgiler.Email, false);

                    Session["Mail"] = bilgiler.Email.ToString();
                    Session["Ad"] = bilgiler.Name.ToString();
                    Session["Soyad"] = bilgiler.Surname.ToString();
                    Session["userid"] = bilgiler.Id.ToString();
                     Session["username"] = bilgiler.Name.ToString();
                    return RedirectToAction("Index", "Home");

                }

            


            ViewBag.Hata = "Kullanıcı Adı Veya Şifreniz Yanlış";
            return View(data);
        }
       
        [HttpPost]
       
        public ActionResult Register(User data)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(data);
                data.Role = "User";

                db.SaveChanges();

                ViewBag.register = "Kayıt işlemi başarılı giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            return View("Login",data);

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("Ad");

            return RedirectToAction("Index","Home");
        }
        public ActionResult SifreReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreReset(string Email)
        {
            var mail = db.Users.Where(x => x.Email == Email).SingleOrDefault();
            
                if (mail!=null)
                {
                    Random rnd = new Random();
                    int yenisifre = rnd.Next();
                    User sifre = new User();
                    mail.Password = (Convert.ToString(yenisifre));
                    db.SaveChanges();
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "kurumsalweb7@gmail.com";
                    WebMail.Password = "kurumsal12345";
                    WebMail.SmtpPort = 587;
                    WebMail.Send(Email, "Giriş Şifreniz", "Şifreniz:" + yenisifre);
                    ViewBag.uyari = "Şifre gönderildi";
                }
                else
                {
                    ViewBag.uyari = "Şifre gönderilmedi";
                }
               


           
            
            return View();
        }
    }
}