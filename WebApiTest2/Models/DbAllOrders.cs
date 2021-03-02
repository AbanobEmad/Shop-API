using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DbAllOrders
    {
        public int id { get; set; }
        public List<DpOrder> orders { get; set; }
        public string S_AR { get; set; }
        public string S_EN { get; set; }
        public float S { get; set; }
        public double PA { get; set; }
        public DateTime Date { get; set; }
    }
}