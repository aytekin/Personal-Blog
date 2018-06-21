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
            AboutPage item = db.aboutInfo.FirstOrDefault();
            ViewBag.userList = db.users.Where(r => r.roles.RoleName != "User").ToList();
            return View(item);
        }
        public ActionResult Gonderiler()
        {
            return View();
        }

        #region 404 Page
        public ActionResult Error404()
        {
            var lastFiveArticle = (from a in db.articles
                                   orderby a.PostedDate descending
                                   select a).Take(3);
            ViewBag.links = lastFiveArticle.ToList();
            return View();
        }
        #endregion

        #region Makaleler
        public ActionResult Makaleler(string Link)
        {
            Articles art = db.articles.Where(a => a.LinkAdress == Link).FirstOrDefault();
            var lastFiveArticle = (from a in db.articles
                                   orderby a.PostedDate descending
                                   select a).Take(3);
            ViewBag.last = lastFiveArticle.ToList();
            if (art != null)
                return View(art);
            else
                return RedirectToAction("Error404");
        }
        #endregion

        #region ParolaChange GET
        public ActionResult Parola(int id)
        {
            Users query = db.users.Where(r => r.UserID == id).FirstOrDefault();

            if (Session["aktif"] != null && query != null)
            {
                return View();
            }
            else
                return RedirectToAction("GirisYap");
        }
        #endregion

        #region ParolaChange 
        [HttpPost]
        public ActionResult Parola(string oldPass,string Password)
        {
            string aktif = Session["aktif"].ToString();
            Users query = db.users.Where(r => r.NickName == aktif).FirstOrDefault();

            Security.Sha1 security = new Sha1();
            string tempPass = security.encoder(oldPass);

            if (query.Password != tempPass && query != null)
            {
                ViewBag.PassDntMatch = "Mevcut Parolanız Hatalıdır";
                return RedirectToAction("Parola", query.UserID);
            }
            else if (Session["aktif"] != null && query != null)
            {

                try
                {
                    query.Password = security.encoder(Password);
                    db.SaveChanges();
                    ViewBag.PassSuccess = "Parolanız başarıyla değiştirilmiştir";
                   

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return RedirectToAction("Profil");
            }
            else
            {
               return RedirectToAction("GirisYap");
            }
                
        }
        #endregion

        #region Profil Düzenle
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            if(Session["aktif"] != null && id > 0)
            {
                Users query = db.users.Where(r => r.UserID == id).FirstOrDefault();
                return View(query);
            }
            return RedirectToAction("GirisYap");
        }
        #endregion

        #region Profil Düzenler POST
        [HttpPost]
        public ActionResult Duzenle(Users u)
        {
            if (Session["aktif"] != null)
            {
                var NickNameControl = db.users.Where(x => x.NickName == u.NickName);
                var EmailContol = db.users.Where(x => x.Email == u.Email);
                
                
                int cnt = 0;
                if (EmailContol != null) cnt = 1;
                if (NickNameControl == null && EmailContol == null)
                {
                    try
                    {
                        Users user = new Users();

                        //formdan gelen model bir user nesnesine yuklendi
                        user.Name = u.Name;
                        user.Surname = u.Surname;
                        user.NickName = u.NickName;
                        user.Email = u.Email;
                       
                        //user nesnesinin veri tabanına güncellemesi gerçekleştirildi
                      
                        db.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                  

                    return RedirectToAction("Profil");


                }
                else
                {
                    //nickName ve email ayırt edici ozellik oldugundan girilen bilgilerle eslesen kayıt veri tabanında varsa hata verecek
                    if (cnt == 1)
                        ViewBag.email = "Girdiğiniz E-Mail Adresi Kayıtlıdır";
                    else
                        ViewBag.nickName = "Kullanıcı Adı Zaten Kullanılmaktadır";
                    return View();
                }
            }
            else
                return RedirectToAction("GirisYap");
        }
        #endregion

        #region UserProfil

        public ActionResult Profil()
        {
            if(Session["aktif"] != null)
            {
                string nick = Session["aktif"].ToString();

                Users query = db.users.Where(x => x.NickName == nick).FirstOrDefault();

                if (query != null)
                    return View(query);
                else
                    return View();

            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Giris yap GET
        [HttpGet]
        public ActionResult GirisYap()
        {
            if (Session["aktif"] == null)
                return View();
            else
                return RedirectToAction("Profil");
        }

        #endregion

        #region GirisYap POST
        [HttpPost]
        public ActionResult GirisYap(string nickName,string pass)
        {
            if (Session["aktif"] == null)
            {
                Users query = db.users.Where(x => x.NickName == nickName).FirstOrDefault();

                if (query != null)
                {
                    Sha1 security = new Sha1();
                    string password = security.encoder(pass);
                    if (password == query.Password && nickName == query.NickName)
                    {
                        Dates d = new Dates();
                        d.DateName = "Login";
                        d.Date = DateTime.Now;
                        d.user = query;
                        db.dates.Add(d);
                        db.SaveChanges();
                        try
                        {
                            db.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                       

                        this.Session["aktif"] = query.NickName;
                        this.Session["Email"] = query.Email;
                        this.Session["surname"] = query.Surname;
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.parola = "Kullanıcı Adı ya da Parola Hatalıdır";
                        ViewBag.nick = nickName;
                        return View();
                    }
                }
                else
                {
                    ViewBag.parola = "Kullanıcı Adı ya da Parola Hatalıdır";
                    ViewBag.nick = nickName;
                    return View();
                }
            }
            else
                return RedirectToAction("Profil");
                
           
        }
        #endregion

        #region Çıkış yap

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        #endregion

        #region KayitOl GET
        [HttpGet]
        public ActionResult KayitOl()
        {
            if (Session["aktif"] == null)
                return View();
            else
                return RedirectToAction("Profil");
        }
        #endregion

        #region KayıtOl POST
        [HttpPost]
        public ActionResult KayitOl(Users u)
        {
            if (Session["aktif"] == null)
            {
                var NickNameControl = db.users.Where(x => x.NickName == u.NickName).FirstOrDefault();
                var EmailContol = db.users.Where(x => x.Email == u.Email).FirstOrDefault();
                Roles rolequery = db.roles.Where(x => x.RoleName == "User").FirstOrDefault();
                
                int cnt = 0;
                if (EmailContol != null) cnt = 1;
                if (NickNameControl == null && EmailContol == null)
                {
                    Users user = new Users();
                    Dates d = new Dates();
                    Sha1 security = new Sha1();
                    string pass = security.encoder(u.Password); //sifre hashlendi


                    try
                    {
                        //formdan gelen model bir user nesnesine yuklendi
                        user.Name = u.Name;
                        user.Surname = u.Surname;
                        user.NickName = u.NickName;
                        user.Email = u.Email;
                        user.Password = pass;
                        user.RoleID = rolequery.RoleID;
                        user.roles = rolequery;
                        d.user = user;
                        d.DateName = "Registration";
                        d.Date = DateTime.Now;
                        db.dates.Add(d);
                        user.dates.Add(d);
                        

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
                    if (cnt == 1)
                        ViewBag.email = "Girdiğiniz E-Mail Adresi Kayıtlıdır";
                    else
                        ViewBag.nickName = "Kullanıcı Adı Zaten Kullanılmaktadır";
                    return View();
                }
            }
            else
                return RedirectToAction("Profil");

            

           
        }
        #endregion

    }
}