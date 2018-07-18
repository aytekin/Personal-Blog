using KisiselBlog.Context;
using KisiselBlog.Models;
using KisiselBlog.Security;
using System;
using KisiselBlog.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace KisiselBlog.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home

       


        #region Anasayfa
        public ActionResult Index()
        {
            ViewBag.year = DateTime.Now.Year;
            return View();
        }

        #endregion

        #region Hakkimizda 
        public ActionResult Hakkimizda()
        {
            AboutPage item = db.aboutInfo.FirstOrDefault();
            ViewBag.userList = db.users.Where(r => r.roles.RoleName != "User").ToList();
            return View(item);
        }

        #endregion

        #region GONDERILER
        [HttpGet]
        public ActionResult Gonderiler()
        {
            return View(db.articles.Where(r => r.Status == true).ToList());
        }

        #endregion

        #region YazarOl
        public ActionResult YazarOl(int id)
        {
            Users u = db.users.Where(r => r.UserID == id).FirstOrDefault();
            if (u != null)
            {
                try
                {
                    u.authorRequest = true;
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    //Bildirim oluşacak
                    db.SaveChanges();
                    ViewBag.Basarili = "Yazar olma isteğiniz başarıyla alınmıştır." +
                        "isteğiniz kısa süre içeresinde değerlendirilecektir." +
                        "ilginiz için teşekkürler.";
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return RedirectToAction("Profil",id);
        }
        #endregion


        #region GONDERILER POST
        [HttpPost]
        public ActionResult Gonderiler(SortingViewModel sorting)
        {
            if(string.IsNullOrEmpty(sorting.Radio) || string.IsNullOrEmpty(sorting.sort))
                return View(db.articles.ToList());
            else
            {
                string sort = sorting.sort;
                string radio = sorting.Radio;

                if(sort == "Tarih")
                {
                    if(radio == "Artan")
                        return View(db.articles.OrderBy(r => r.PostedDate));
                    else
                        return View(db.articles.OrderByDescending(r => r.PostedDate));
                }
                else
                {
                    if (radio == "Artan")
                        return View(db.articles.OrderBy(r => r.author.NickName));
                    else
                        return View(db.articles.OrderByDescending(r => r.author.NickName));

                }
                
            }
           
        }

        #endregion

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
            if (art != null && art.Status)
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
                return View(query);
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
        public ActionResult Duzenle(ProfileEditViewModel u, HttpPostedFileBase PPPath)
        {
            if (Session["aktif"] != null)
            {
                string userr = Session["aktif"].ToString();
                Users us = db.users.Where(a => a.NickName == userr).FirstOrDefault();
                Users NickNameControl = db.users.Where(x => x.NickName == u.NickName).FirstOrDefault();
                Users EmailContol = db.users.Where(x => x.Email == u.Email).FirstOrDefault();


                if (PPPath == null && u != null)
                {
                    if (NickNameControl != null && us.UserID != NickNameControl.UserID)
                    {
                        //Kullanicinin degistirmek istedigi nickname kullaniliyor ve kullanan bu sahis degil
                        ViewBag.ExitsNickname = "Girdiğiniz Kullanıcı Adı Zaten Kullanılmaktadır.";

                    }
                    else if (EmailContol != null && us.UserID != EmailContol.UserID)
                    {
                        //Kullanicinin degistirmek istedigi email adresi kullaniliyor ve kullanan bu sahis degil
                        ViewBag.ExitsEmail = "Girdiğiniz E-mail Adresi Zaten Kullanılmaktadır.";
                    }
                    else
                    {
                        try
                        {
                            //formdan gelen model bir user nesnesine yuklendi
                            us.Name = u.Name;
                            us.Surname = u.Surname;
                            us.NickName = u.NickName;
                            us.Email = u.Email;
                            us.AboutUser = u.About;
                            //user nesnesinin veri tabanına güncellemesi gerçekleştirildi

                            db.SaveChanges();

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                
                }
                else if(PPPath != null)
                {
                    try
                    {
                        //formdan gelen model bir user nesnesine yuklendi
                        us.PPPath = ImageAddProfil(PPPath);
                        //user nesnesinin veri tabanına güncellemesi gerçekleştirildi
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return RedirectToAction("Duzenle", us.UserID);



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
        public ActionResult GirisYap(LoginViewModel model)
        {
           
            if (Session["aktif"] == null)
            {
                string nickName, pass;
                nickName = pass = "";
                pass = model.pass;
                nickName = model.nickName;
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
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.parola = "Kullanıcı Adı ya da Parola Hatalıdır";
                    return View(model);
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
            {
                Users u = null;
                return View(u);
            }
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
                
                if (EmailContol != null)
                {
                    string message = "Girdiğiniz E-mail adresi sisteme kayıtlıdır.\n Lütfen farklı bir e-mail adresi deneyiniz.";
                    ViewBag.Hata2 = message;
                    return View(u);
                }
                else if(NickNameControl != null)
                {
                    string message = "Girdiğiniz kullanıcı adı sisteme kayıtlıdır.\n Lütfen farklı bir kullanıcı adı deneyiniz.";
                    ViewBag.Hata = message;
                    return View(u);
                }
                else
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
                    KisiselBlog.Models.ViewModels.LoginViewModel login = new LoginViewModel();
                    login.nickName = u.NickName;
                    login.pass = u.Password;
                    GirisYap(login);

                    return RedirectToAction("Index");


                }
               
            }
            else
                return RedirectToAction("Profil");

            

           
        }
        #endregion

        #region ImageAdd function Profil Photos
        private string ImageAddProfil(HttpPostedFileBase i)
        {
            Image image = Image.FromStream(i.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 200, Height = 200 });
            string uzanti = System.IO.Path.GetExtension(i.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/media/profil/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }

        #endregion

    }
}