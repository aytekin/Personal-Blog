using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models.ViewModels
{
    public class SortingViewModel
    {
        public string sort { get; set; }
        public string Radio { get; set; }
    }

    public class LoginViewModel
    {
        public string nickName { get; set; }
        public string pass { get; set; }
        public bool remember { get; set; }
    }

    public class ProfileEditViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
       
    }
}