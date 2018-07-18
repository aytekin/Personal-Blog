using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KisiselBlog.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KisiselBlog.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("KisiselBlog")
        {

        }

        public DbSet<Users> users { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Comments> comments { get; set; }
       // public DbSet<Images> images { get; set; }
        public DbSet<Articles> articles { get; set; }
        public DbSet<Dates> dates { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<AboutPage> aboutInfo { get; set; }
        public DbSet<SubComment> subComments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)//Kayit(s) Falan oluşturmasını engellemek için ezdirme yaptık
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Users>().Property(b => b.PPPath).IsOptional(); //NickName attr is not required on comment table
            modelBuilder.Entity<Users>().Property(b => b.AboutUser).IsOptional();
            
        }
       

    }
  


}