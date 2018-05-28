using KisiselBlog.Context;
using System;
using System.Collections.Generic;
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

      
    }
}