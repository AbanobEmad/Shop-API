using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Seller")]
    public class Seller
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public ICollection<Product> products { get; set; }
    }
}