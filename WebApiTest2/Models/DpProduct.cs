using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DpProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public bool Offer { get; set; }
        public float percent { get; set; }
        public int Category_TD { get; set; }
        public int Seller_ID { get; set; }
        public int Type_ID { get; set; }
        public bool Save { get; set; }
        public List<string> Image_path { get; set; }
        public List<DpSize> SOfP { get; set; }
    }
}