using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Image_Path")]
    public class Image_Path
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public ICollection<Product_Image_path> product_Images { get; set; }
    }
}