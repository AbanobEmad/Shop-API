using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Category_Gender")]
    public class Category_Gender
    {
        public int ID { get; set; }
        public int Categoer_ID { get; set; }
        public int Gender_ID { get; set; }
        [ForeignKey("Categoer_ID")]
        public virtual Category category { get; set; }
        [ForeignKey("Gender_ID")]
        public virtual Gender gender { get; set; }
    }
}