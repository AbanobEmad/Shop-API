using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Approval { get; set; }
        public bool Show_Home { get; set; }
        public float Discount { get; set; }
        public bool Offer { get; set; }
        public float percent { get; set; }
        public int Category_ID { get; set; }
        public int Seller_ID { get; set; }
        public int Type_ID { get; set; }
        public bool Save { get; set; }
        public float Shipping { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category category { get; set; }
        [ForeignKey("Type_ID")]
        public virtual Type tybe { get; set; }
        [ForeignKey("Seller_ID")]
        public virtual Seller seller { get; set; }
        public ICollection<Product_Image_path> product_Images { get; set; }

        public ICollection<Save_Product> save_Products { get; set; }
        public ICollection<SizeOFProduct> sizeOFProducts { get; set; }
        public ICollection<Cart> carts { get; set; }
        public ICollection<Single_Order> Single_Orders { get; set; }
    }
}