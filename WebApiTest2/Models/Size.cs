using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Size")]
    public class Size
    {
        public int ID { get; set; }
        public string size { get; set; }
        public ICollection<SizeOFProduct> sizeOFProducts { get; set; }
        public ICollection<Cart> carts { get; set; }
        public ICollection<Single_Order> Single_Orders { get; set; }
        public ICollection<CategorySize> categorySizes { get; set; }
    }
}