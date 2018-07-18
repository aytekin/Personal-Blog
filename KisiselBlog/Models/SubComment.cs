using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    [Table("SubComment")]
    public class SubComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCommentID { get; set; }
        [StringLength(400), Required]
        public string Content { get; set; }
        [StringLength(32), Required]
        public string UserName { get; set; }
        [StringLength(32), Required]
        public string UserSurname { get; set; }
        [StringLength(64), Required]
        public string UserEmail { get; set; }
        public string UserPhoto { get; set; }
        public DateTime AddedDate { get; set; }

        public int CommentID { get; set; }
        public virtual Comments topComment { get; set; }
    }
}