using KisiselBlog.Context;
using KisiselBlog.Models;
using KisiselBlog.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.year = DateTime.Now.Year;
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Gonderiler()
        {
            return View();
        }

        #region Giris yap GET
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }

        #endregion

        #region GirisYap POST
        [HttpPost]
        public ActionResult GirisYap(string nickName,string pass)
        {
            return View();
        }
        #endregion




        #region KayitOl GET
        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }
        #endregion


        #region KayıtOl POST
        [HttpPost]
        public ActionResult KayitOl(Users u)
        {
            var NickNameControl = db.users.Where(x => x.NickName == u.NickName).FirstOrDefault();
            var EmailContol = db.users.Where(x => x.Email == u.Email).FirstOrDefault();
            Roles rolequery = db.roles.Where(x => x.RoleName == "User").FirstOrDefault();

            int cnt = 0;
            if (EmailContol != null) cnt = 1;
            if(NickNameControl == null || EmailContol == null)
            {
                Users user = new Users();
                Sha1 security = new Sha1();
                string pass = security.encoder(u.Password); //sifre hashlendi

               
                try
                {
                    //formdan gelen model bir user nesnesine yuklendi
                    user.Name = u.Name;
                    user.Surname = "Erlale"; //  u.Surname;
                    user.NickName = u.NickName;
                    user.Email = u.Email;
                    user.Password = pass;
                    user.LastLoginDate = DateTime.Now;
                    user.RoleID = rolequery.RoleID;
                    user.roles = rolequery;

                    //user nesnesinin veri tabanına kaydı gerçekleştirildi
                    db.users.Add(user);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); 
                }

                //kayıt yapıldıktan sonra login islemi gerceklestiriliyor
                GirisYap(u.NickName, u.Password);

                return RedirectToAction("Index");


            }
            else
            {
                //nickName ve email ayırt edici ozellik oldugundan girilen bilgilerle eslesen kayıt veri tabanında varsa hata verecek
                if (cnt == 0)
                    ViewBag.email = "E-Mail Adresi Zaten Kullanılmaktadır.";
                else
                    ViewBag.nickName = "Kullanıcı Adı Zaten Kullanılmaktadır.";
                return View();
            }

           
        }
        #endregion

    }
}