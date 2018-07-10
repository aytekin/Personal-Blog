using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models.ViewModels
{
    public class SortingViewModel
    {
        public string type { get; set; }
        public bool isitgrowing { get; set; }
    }

    public class LoginViewModel
    {
        public string nickName { get; set; }
        public string pass { get; set; }
        public bool remember { get; set; }
    }
}