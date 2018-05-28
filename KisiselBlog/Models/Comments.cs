using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    [Table("Comments")]
    public class Comments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        [StringLength(400), Required]
        public string Text { get; set; }
        [StringLength(15), Required]
        public string Name { get; set; }
        [StringLength(15), Required]
        public string Surname { get; set; }
        [StringLength(15), Required]
        public string Email { get; set; }
        [StringLength(15)]
        public string NickName { get; set; }

        public int ArticleID { get; set; }
        public virtual Articles article { get; set; }

   

    }
}