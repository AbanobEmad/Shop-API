using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DbAllCarts
    {
        public List<DbCartOfProduct> Cart { get; set; }
        public float S { get; set; }
        public double PA { get; set; }
    }
}