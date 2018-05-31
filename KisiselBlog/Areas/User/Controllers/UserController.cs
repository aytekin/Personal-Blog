using KisiselBlog.Context;
using KisiselBlog.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult Kullanicilar()
        {
            var item = db.users.ToList();
            return View(item);
        }
    }
}