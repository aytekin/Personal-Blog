using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    public class Dates
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary key and autoincrement 
        public int DateID { get; set; }
        [StringLength(15)]
        public string DateName { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public virtual Users user { get; set; }
    }
}