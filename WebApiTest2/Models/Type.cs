using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Type")]
    public class Type
    {
        public int ID { get; set; }
        public string Type_Name_AR { get; set; }
        public string Type_Name_EN { get; set; }
        public ICollection<Product> products { get; set; }
    }
}