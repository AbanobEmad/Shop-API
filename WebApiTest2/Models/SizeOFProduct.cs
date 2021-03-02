using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("SizeOFProduct")]
    public class SizeOFProduct
    {
        public int ID { get; set; }
        public int Size_ID { get; set; }
        public int Product_ID { get; set; }
        public int Max_C { get; set; }
        [ForeignKey("Product_ID")]
        public virtual Product product { get; set; }
        [ForeignKey("Size_ID")]
        public virtual Size size { get; set; }
    }
}