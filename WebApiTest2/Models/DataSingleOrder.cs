using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DataSingleOrder
    {
        public int ID { get; set; }
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public string Size_Name { get; set; }
        public int Count { get; set; }
        public string Seller_Name { get; set; }
        public string Seller_Phone { get; set; }
    }
}