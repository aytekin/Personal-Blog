using KisiselBlog.Areas.User.Models;
using KisiselBlog.Context;
using KisiselBlog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace KisiselBlog.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //database object
        DatabaseContext db = new DatabaseContext();
        // GET: User/User
        public ActionResult Profil()
        {
            return View();
        }

        #region KullaniciYetkilendir
        [HttpPost]
        public ActionResult KullaniciYetki(AuthorizeModel model)
        {
            int id, roleid;
            id = roleid = 0;
            roleid =model.formrole;
            id = model.userid;

            Roles role = db.roles.Where(r => r.RoleID == roleid).FirstOrDefault();
            Users user = db.users.Where(u => u.UserID == id).FirstOrDefault();
            
            if(role != null && user != null)
            {
                //rol ve kullanıcı hatasız geldiyse rolu esitliyoruz
                try
                {
                    user.roles = role;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            
            return RedirectToAction("Kullanicilar");
        }
        #endregion

        #region KullaniciOnayla
        public ActionResult KullaniciOnayla(int id)
        {
            Users u = db.users.Where(r => r.UserID == id).FirstOrDefault();
            Roles rolequery = db.roles.Where(x => x.RoleName == "Author").FirstOrDefault();

            if (u != null)
            {
                if (u.authorRequest)
                {
                    try
                    {
                        u.RoleID = rolequery.RoleID;
                        u.roles = rolequery;
                        u.authorRequest = false;
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return RedirectToAction("Kullanicilar");
        }
        #endregion

        #region Delete User

        public ActionResult KullaniciSil(int id)
        {
            KisiselBlog.Models.Users u = db.users.Where(r => r.UserID == id).FirstOrDefault();
            if (u != null)
            {
                try
                {
                    db.users.Remove(u);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Kullanicilar");
        }


        #endregion

        #region Confirm Article

        public ActionResult MakaleOnayla(int id)
        {
            KisiselBlog.Models.Articles u = db.articles.Where(r => r.ArticleID  == id).FirstOrDefault();
            if (u != null)
            {
                try
                {
                    //makale onaylandı
                    u.Status = true;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToAction("Makaleler");
        }


        #endregion

        #region MakaleSil
        public ActionResult MakaleSil(int id)
        {
            Articles delete = db.articles.Where(a => a.ArticleID == id).FirstOrDefault();
            if(delete != null)
            {
                try
                {
                    db.articles.Remove(delete);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return RedirectToAction("Makaleler");
        }
        #endregion

        #region Makaleler
        //admin için makaleler onaylama vs
        [HttpGet]
        public ActionResult Makaleler()
        {

            return View(db.articles.OrderBy(r => r.Status).ToList());
        }
        #endregion
        
        #region aboutPageInfo GET
        [HttpGet]
        public ActionResult HakkimizdaEkle()
        {
            AboutPage about = db.aboutInfo.FirstOrDefault();
            if (about == null)
                return View();
            else
            {
                AddAboutInfoModel model = new AddAboutInfoModel();
                model.Header = about.Header;
                model.imagePath = about.imagePath;
                model.About = about.About;
                return View(model);
            }
        }
        #endregion

        #region aboutPageInfo POST
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult HakkimizdaEkle(AddAboutInfoModel model, HttpPostedFileBase imagePath)
        {
            // var nick = Session["aktif"].ToString();
            if(model.About != null)
            {
                try
                {
                    AboutPage about = db.aboutInfo.FirstOrDefault();
                    if (about == null)
                    {
                        AboutPage a = new AboutPage();
                        a.About = model.About;
                        a.Header = model.Header;
                        if (imagePath != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath(a.imagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath(a.imagePath));
                            }
                            a.imagePath = ImageAddAbout(imagePath);

                        }
                        else
                            a.imagePath = null; 
                        db.aboutInfo.Add(a);

                    }
                    else
                    {
                       
                        about.About = model.About;
                        if (imagePath != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath(about.imagePath)))
                            {
                                System.IO.File.Delete(Server.MapPath(about.imagePath));
                            }
                            about.imagePath = ImageAddAbout(imagePath);

                        }
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.ds = "Hata";
                }
                return RedirectToAction("HakkimizdaEkle");

            }
            return View();
        }
        #endregion

        #region Kullanicilar
        public ActionResult Kullanicilar()
        {
            var item = db.users.ToList();
            return View(item);
        }

        #endregion

        #region AddCategories GET
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            var list = db.categories.ToList();
            return View(list);
        }
        #endregion

        #region KategoriSil GET
        public ActionResult KategoriSil(int id)
        {
            Categories c = db.categories.Where(r => r.CategoryID == id).FirstOrDefault();
            if(c != null)
            {
                try
                {
                    db.categories.Remove(c);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                ViewBag.idnull = "Silmek istediğiniz etiket bulunamadı";
            }
            return RedirectToAction("KategoriEkle");
        }
        #endregion

        #region AddCategories POST
        [HttpPost]
        public ActionResult KategoriEkle(AddCategoriesModel model)
        {
            //gelen model kaydedilecek
            var query = db.categories.Where(c => c.CategoryName == model.CategoryName).FirstOrDefault(); 
            if(query == null)
            {
                try
                {
                    Categories c = new Categories();
                    c.CategoryName = model.CategoryName;
                    db.categories.Add(c);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            else
            {
                ViewBag.c = model.CategoryName.ToString() + " etiketi zaten mevcut";
            }
            return View(db.categories.ToList());
        }
        #endregion

        #region MakaleEkle GET
        [HttpGet]
        public ActionResult MakaleEkle()
        {
            return View();
        }
        #endregion

        #region MakeleEkle POST
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult MakaleEkle(AddArticleModel model, HttpPostedFileBase ImagesPath)
        {
          if(Session["aktif"] != null)
          {
                var nick = Session["aktif"].ToString();
                Users query = db.users.Where(r => r.NickName == nick).FirstOrDefault();
                Articles art = new Articles();
                art.PhotoPath = ImageAdd(ImagesPath);
                art.Header = model.Head;
                art.LinkAdress = model.Link;


                art.Text = model.Text;
                art.Status = false;
                art.PostedDate = DateTime.Now;

                art.author = query;
                /*query.articles.Add(art); */
                db.articles.Add(art);
                db.SaveChanges();
                return RedirectToAction("Profil");
            }
            else
            {
                //giriş yap olacak burası
                return RedirectToAction("Profil");
            }
              
        
        }
        #endregion

        #region Makalelerim
        [HttpGet]
        public ActionResult Makalelerim()
        {
            if (Session["aktif"] != null)
            {
                string k_adi = Session["aktif"].ToString();

                Users user = db.users.Where(u => u.NickName == k_adi).FirstOrDefault();
                
                if(user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region ImageAdd function Makale
        private string ImageAdd(HttpPostedFileBase i)
        {
            Image image = Image.FromStream(i.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 900, Height = 400 });
            string uzanti = System.IO.Path.GetExtension(i.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/media/img/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }

        #endregion

        #region ImageAdd function About
        private string ImageAddAbout(HttpPostedFileBase i)
        {
            Image image = Image.FromStream(i.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 750, Height = 450 });
            string uzanti = System.IO.Path.GetExtension(i.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/media/about/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }

        #endregion
    }
}