using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Single_Order")]
    public class Single_Order
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Count { get; set; }
        public int Orders_Id { get; set; }
        public int? Size_Id { get; set; }

        [ForeignKey("Product_ID")]
        public virtual Product product { get; set; }
        [ForeignKey("Size_Id")]
        public virtual Size Size { get; set; }
        [ForeignKey("Orders_Id")]
        public virtual Orders Orders { get; set; }
    }
}