using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    [Table("Roles")]
    public class Roles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary key and autoincrement 
        public int RoleID { get; set; }
        [StringLength(11)]
        public string RoleName { get; set; }
        public virtual ICollection<Users> users { get; set; }


    }
}