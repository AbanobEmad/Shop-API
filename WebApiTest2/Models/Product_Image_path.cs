using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Product_Image_path")]
    public class Product_Image_path
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Image_path_ID { get; set; }
        [ForeignKey("Product_ID")]
        public virtual Product product { get; set; }
        [ForeignKey("Image_path_ID")]
        public virtual  Image_Path Image_Path { get; set; }
    }
}