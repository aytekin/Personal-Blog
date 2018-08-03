using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KisiselBlog.Models
{
    [Table("Articles")]
    public class Articles
    {
        public Articles()
        {
            this.category = new HashSet<Categories>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleID { get; set; }
        [StringLength(64),Required]
        public string Header { get; set; }
        [StringLength(64), Required]
        public string LinkAdress { get; set; }
        public DateTime PostedDate { get; set; }
        [StringLength(5000), Required]
        [AllowHtml]
        public string Text { get; set; }
        [StringLength(180), Required]
        public string PhotoPath { get; set; }
        [Required]
        public Boolean Status { get; set; }

        public int UserID { get; set; }
        public virtual Users author { get; set; }

        public virtual ICollection<Comments> comments { get; set; }
        public virtual ICollection<Categories> category { get; set; }
    }
}