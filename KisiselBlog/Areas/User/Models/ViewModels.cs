using KisiselBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.Areas.User.Models
{
    public class AddArticleModel
    {
        [Required]
        [StringLength(20,ErrorMessage ="Başlık {2} ile {0} karakter arasında olmalır",MinimumLength =5)]
        public string Head { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "Başlık {2} ile {0} karakter arasında olmalır",MinimumLength =100)]
        [AllowHtml]
        public string Text { get; set; }
        [Required]
        public string Categories { get; set; }


    }

    public class AddAboutInfoModel
    {
        [Required]
        public string imagePath { get; set; }
        [Required]
        [StringLength(25)]
        public string Header { get; set; }
        [Required]
        [StringLength(600)]
        public string About { get; set; }
    }
    public class AddCategoriesModel
    {
    
        [StringLength(15), Required]
        public string CategoryName { get; set; }
    }
    public class AuthorizeModel
    {

        public int formrole { get; set; }
        public int userid { get; set; }
    }
}