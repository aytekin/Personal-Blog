using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    [Table("Images")]
    public class Images
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        [Required , StringLength(250)]
        public string ImagePath { get; set; }
        public int ArticleID { get; set; }
        public virtual Articles article { get; set; }

    }
}