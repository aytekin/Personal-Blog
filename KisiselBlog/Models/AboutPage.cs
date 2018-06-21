using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KisiselBlog.Models
{
    [Table("AboutPage")]
    public class AboutPage
    {
        [Key]
        public int AboutID { get; set; }
        [Required]
        public string imagePath { get; set; }
        [Required]
        [StringLength(25)]
        public string Header { get; set; }
        [Required]
        [StringLength(600)]
        public string  About { get; set; }
    }
}