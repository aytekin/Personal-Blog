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
            routes.MapRoute("cikis", "CikisYap", new { controller = "Home", action = "CikisYap" });
            routes.MapRoute("anasayfa", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("anaSayfa3", "index", new { controller = "Home", action = "Index" });
            routes.MapRoute("anaSayfa2", "Anasayfa", new { controller = "Home", action = "Index" });
            routes.MapRoute("hakkimizda", "Hakkimizda", new { controller = "Home", action = "Hakkimizda" });
            routes.MapRoute("profil", "Kullanıcı/Profil", new {controller = "Home", action = "Profil" });
            routes.MapRoute("profilduzenle", "Kullanıcı/Duzenle/{id}", new {controller = "Home", action = "Duzenle"  });
            routes.MapRoute("paroladegistir", "Parola/{id}", new {controller = "Home", action = "Parola"  });
            routes.MapRoute("adminDash", "Admin", new { controller = "User", action = "Profil", }).DataTokens.Add("area", "User");
            routes.MapRoute("kullanicilar", "Admin/Users", new { controller = "User", action = "Kullanicilar", }).DataTokens.Add("area", "User");
            routes.MapRoute("makaleEkle", "Admin/MakaleEkle", new { controller = "User", action = "MakaleEkle", }).DataTokens.Add("area", "User");
            routes.MapRoute("KullaniciSil", "Admin/KullaniSil/{id}", new { controller = "User", action = "KullaniciSil", }).DataTokens.Add("area", "User");
            routes.MapRoute("kategoriEkle", "Admin/KategoriEkle", new { controller = "User", action = "KategoriEkle", }).DataTokens.Add("area", "User");
            routes.MapRoute("hakkimizdaadmin", "Admin/Hakkimizda", new { controller = "User", action = "HakkimizdaEkle", }).DataTokens.Add("area", "User");
            routes.MapRoute("makaleler", "Makaleler/{Link}", new { controller = "Home", action = "Makaleler", });
            routes.MapRoute("gonderiler", "Makaleler", new { controller = "Home", action = "Gonderiler", });
            routes.MapRoute("error404", "Error404", new { controller = "Home", action = "Error404", });
            routes.MapRoute("404-PageNotFound","{*url}",new { controller = "Home", action = "Error404" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }

    
       
    }
}
