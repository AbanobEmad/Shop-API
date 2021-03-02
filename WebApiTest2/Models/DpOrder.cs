using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DpOrder
    {
        public int Id { get; set; }
        public int? S_Id { get; set; }
        public string S_N { get; set; }
        public int C { get; set; }
        public DpProduct p { get; set; }
    }
}