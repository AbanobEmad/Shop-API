using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext () : base("CS")
        {

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Type>Types { get; set; }
        public virtual DbSet<Gender>Genders { get; set; }
        public virtual DbSet<Category_Gender> Category_Genders { get; set; }
        public virtual DbSet<Image_Path> Image_Paths { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Image_path> Product_Images { get; set; }
        public virtual DbSet<DataUser> DataUsers { get; set; }
        public virtual DbSet<Save_Product> Save_Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<SizeOFProduct> SizeOFProducts { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Single_Order> Single_Orders { get; set; }
    }
}