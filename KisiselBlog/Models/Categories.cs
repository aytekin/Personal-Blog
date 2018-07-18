using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    public class Categories
    {
        public Categories()
        {
            this.articles = new HashSet<Articles>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [StringLength(64),Required]
        public string CategoryName { get; set; }
        public virtual ICollection<Articles> articles { get; set; }
    }
}