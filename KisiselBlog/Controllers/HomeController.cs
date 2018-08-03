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
using System.Net.Mail;
using System.Net;

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

        #region SaveComment
        [HttpPost]
        public ActionResult YorumYap(AddCommentViewModel model)
        {
           if(Session["aktif"] != null)
           {
                string userNick = Session["aktif"].ToString();
                string commentContent = model.Comment;
                int articleid = model.articleid;

                Users user = db.users.Where(u => u.NickName == userNick).FirstOrDefault();
                Articles article = db.articles.Where(a => a.ArticleID == articleid).FirstOrDefault();

                if(user != null && article != null)
                {
                    Comments comment = new Comments();

                    try
                    {
                        comment.article = article;
                        comment.Content = commentContent;
                        comment.UserPhoto = user.CommentPPPath;
                        comment.UserName = user.Name;
                        comment.UserSurname = user.Surname;
                        comment.UserEmail = user.Email;
                        comment.AddedDate = DateTime.Now;

                        db.comments.Add(comment);
                        db.SaveChanges();
                        return RedirectToAction(article.LinkAdress, "Makaleler");
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return RedirectToAction("GirisYap");
            }
           else
           {
              return RedirectToAction("GirisYap");
           }
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
            var comments = art.comments.ToList();
            ViewBag.comments = comments;
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

        #region Profil Düzenle POST
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
                            if (u.Twitter != null)
                                us.UserTwitterAdress = "https://www.twitter.com/" + u.Twitter;
                            else
                                us.UserTwitterAdress = null;
                            if (u.Github != null)
                                us.UserGithubAdress = "https://www.github.com/" + u.Github;
                            else
                                us.UserGithubAdress = null;
                            if (u.Bitbucket != null)
                                us.UserBitbucketAdress = "https://bitbucket.org/" + u.Bitbucket;
                            else
                                us.UserBitbucketAdress = null;
                            if (u.Linkedin != null)
                                us.UserlinkedinAdress = "https://www.linkedin.com/in/" + u.Linkedin;
                            else
                                us.UserlinkedinAdress = null;
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
                        if (System.IO.File.Exists(Server.MapPath(us.PPPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(us.PPPath));
                        }
                        if (System.IO.File.Exists(Server.MapPath(us.CommentPPPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(us.CommentPPPath));
                        }
                        us.PPPath = ImageAddProfil(PPPath);
                        us.CommentPPPath = ImageAddComment(PPPath);
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
                        if (query.ControlCodeStatus)
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
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }


                            this.Session["aktif"] = query.NickName;
                            this.Session["Email"] = query.Email;
                            this.Session["surname"] = query.Surname;
                            this.Session["yetki"] = query.roles.RoleName;

                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ViewBag.HesapOnaylanmamis = "Hesabınız Henüz Onaylanmadığı İçin Giriş Yapamazsınız\r\n" +
                                "Mail Aresinize Gönderilen Hesap Onaylama Bağlantısına Tıklayın.";
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

                        Guid Kontrol;
                        Kontrol = Guid.NewGuid();
                        //Confirm Email Send
                        #region mailGonderme
                        var fromAddress = new MailAddress("infosautoyota@gmail.com", "CodeTech");
                        var toAddress = new MailAddress(user.Email, "To Name");
                        const string fromPassword = "Toyota12*";
                        string subject = "CodeTech'e Hoşgeldiniz";
                        string body = "Merhaba Ayekin Erlale ,\r\n\r\n" +
                        "Üye olma işleminiz başarıyla gerçekleşmiştir.\r\nAramıza Katıldığınız için teşekkür ederiz." +
                        "\r\nHesabınızı aktif hale getirmek için aşağıdaki linke tıklayın\r\n\r\n" + "http://localhost:59052/ConfirmAccount/" + user.ControlCode.ToString();
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                        };
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtp.Send(message);
                        }

                        #endregion

                        user.ControlCode = Kontrol;
                        user.ControlCodeStatus = false;

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

                    

                    return RedirectToAction("Index");


                }
               
            }
            else
                return RedirectToAction("Profil");

            

           
        }
        #endregion

        #region HesapOnayı
        [HttpGet]
        public ActionResult HesapOnayi(string guid)
        {
            Users user = db.users.Where(
                u => u.ControlCode.ToString() == guid).FirstOrDefault();

            if(user != null)
            {
                if (!user.ControlCodeStatus)
                {
                    user.ControlCodeStatus = true;
                    db.SaveChanges();
                    ViewBag.HesapOnaylandi = "Aramıza Hoşgeldin " + user.Name + " " + user.Surname +
                        " Hesabın Onaylandı";
                }
                else
                {
                    ViewBag.ZatenOnayli =  user.Name + " " + user.Surname +
                        " Hesabın Zaten Onaylanmış.";
                }
                return View();
            }

            return RedirectToAction("Index");

            
        }

        #endregion

        #region Parolamı Sıfırla
        [HttpGet]
        public ActionResult ParolaSifirla(string guid)
        {
            Users user = db.users.Where(u => u.ControlCode.ToString() == guid).FirstOrDefault();

            if(user != null)
            {
                return View();
            }


            return RedirectToAction("Index");
        }
        #endregion
             
        #region Parolamı Sıfırla Post
        [HttpPost]
        public ActionResult ParolaSifirla(string guid,string Password)
        {
            Users user = db.users.Where(u => u.ControlCode.ToString() == guid).FirstOrDefault();

            if(user != null)
            {
                Security.Sha1 hash = new Sha1();
                user.Password = hash.encoder(Password);
                user.ControlCode = Guid.NewGuid();
                db.SaveChanges();

                return RedirectToAction("GirisYap");
            }


            return RedirectToAction("Index");
        }
        #endregion

        #region Parolamı Unuttum
        [HttpGet]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }
        #endregion 
        
        #region Parolamı Unuttum POST
        [HttpPost]
        public ActionResult ParolamiUnuttum(string email)
        {
            Users user = db.users.Where(u => u.Email == email).FirstOrDefault();

            if(user != null)
            {
                user.ControlCode = Guid.NewGuid();
                db.SaveChanges();


                #region mailGonderme
                var fromAddress = new MailAddress("infosautoyota@gmail.com", "CodeTech");
                var toAddress = new MailAddress(email, "To Name");
                const string fromPassword = "Toyota12*";
                string subject = "CodeTech'e Kullanıcı Parola Sıfırlama";
                string body = "Merhaba "+ user.NickName +" ,\r\n\r\n" +
                    "Parolanı Sıfırlamak İçin Aşağıdaki Linke Tıkla." +
                    "\r\n\r\n" + "http://localhost:59052/ParolaSifirla/" + user.ControlCode.ToString();
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                #endregion
                ViewBag.MailGonderildi = "Girdiğiniz E-mail Adresine Parola Sıfırlama Bağlantısı Gönderildi";
                return View();
            }
            else
            {
                ViewBag.KullaniciBulunamadi = "Girdiğiniz E-mail Adresine Ait Sistemimizde Kullanici Bulunamadi.";
                return View();
            }

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

        #region ImageAdd function Profil Photos for Commnets
        private string ImageAddComment(HttpPostedFileBase i)
        {
            Image image = Image.FromStream(i.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 50, Height = 50 });
            string uzanti = System.IO.Path.GetExtension(i.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/media/CommentPht/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }

        #endregion

    }
}