using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("CategorySize")]
    public class CategorySize
    {
        public int ID { get; set; }
        public int Categoer_ID { get; set; }
        public int Size_ID { get; set; }
        [ForeignKey("Categoer_ID")]
        public virtual Category category { get; set; }
        [ForeignKey("Size_ID")]
        public virtual Size Size { get; set; }
    }
}