using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DbCartOfProduct
    {
        public int Id { get; set; }
        public DpProduct p { get; set; }
        public int? S_Id { get; set; }
        public string S_N { get; set; }
        public int M_C { get; set; }
        public int C { get; set; }
    }
}