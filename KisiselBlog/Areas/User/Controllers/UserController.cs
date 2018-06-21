using KisiselBlog.Areas.User.Models;
using KisiselBlog.Context;
using KisiselBlog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [HttpPost]
        public ActionResult HakkimizdaEkle(AddAboutInfoModel model, HttpPostedFileBase imagePath)
        {
            // var nick = Session["aktif"].ToString();
            try
            {
                AboutPage about = db.aboutInfo.FirstOrDefault();
                if (about == null)
                {
                    AboutPage a = new AboutPage();
                    a.About = model.About;
                    a.Header = model.Header;
                    a.imagePath = ImageAddAbout(imagePath);
                    db.aboutInfo.Add(a);

                }
                else
                {
                    about.About = model.About;
                    about.Header = model.Header;
                    about.imagePath = ImageAddAbout(imagePath);
                }
                db.SaveChanges();
            }
            catch(Exception e)
            {
                //exceptions
            }
           
            return RedirectToAction("Profil");
        }
        #endregion

        #region Kullanicilar
        public ActionResult Kullanicilar()
        {
            var item = db.users.ToList();
            foreach (var i in item)
            {
                DateTime d = i.dates.Max(r => r.Date);
                Console.WriteLine(d.ToString());
            }
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

        #region AddCategories GET
        [HttpGet]
        public ActionResult Kategoriler()
        {
            
            return View();
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
                return RedirectToAction("");
            }
            else
            {
                ViewBag.c = model.CategoryName.ToString() + "Kategori zaten mevcut";
            }
            return View();
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
        [HttpPost]
        public ActionResult MakaleEkle(AddArticleModel model, HttpPostedFileBase ImagesPath)
        {
          
              // var nick = Session["aktif"].ToString();
                Users query = db.users.Where(r => r.UserID == 1).FirstOrDefault();
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