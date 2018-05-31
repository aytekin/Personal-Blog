using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KisiselBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routing
            routes.MapRoute("kaydol", "Kaydol", new { controller = "Home", action = "KayitOl" });
            routes.MapRoute("giris", "Giris", new { controller = "Home", action = "GirisYap" });
            routes.MapRoute("anasayfa", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("anaSayfa3", "index", new { controller = "Home", action = "Index" });
            routes.MapRoute("anaSayfa2", "Anasayfa", new { controller = "Home", action = "Index" });
            routes.MapRoute("hakkimizda", "Hakkimizda", new { controller = "Home", action = "Hakkimizda" });
            routes.MapRoute("profil", "Kullanıcı/Profil", new {controller = "Home", action = "Profil" });
            routes.MapRoute("profilduzenle", "Kullanıcı/Duzenle/{id}", new {controller = "Home", action = "Duzenle"  });
            routes.MapRoute("paroladegistir", "Parola/{id}", new {controller = "Home", action = "Parola"  });
            routes.MapRoute("adminDash", "Admin", new { controller = "User", action = "Profil", }).DataTokens.Add("area", "User");
            routes.MapRoute("adminkullanicilar", "Admin/Users", new { controller = "User", action = "Kullanicilar", }).DataTokens.Add("area", "User");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }

    
       
    }
}
