using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DataOrder
    {
        public int ID { get; set; }
        public string Name_User { get; set; }
        public string Phone_User { get; set; }
        public string Address_User { get; set; }
        public string State { get; set; }
        public double Order_Price { get; set; }
        public float Order_Shapping { get; set; }

    }
}