using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KisiselBlog.Security
{
    [Serializable]
    public class SessionModel
    {


        public string NickName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


    
    }
}