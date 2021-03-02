using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Gender")]
    public class Gender
    {
        public int ID { get; set; }
        public string Gender_Name_AR { get; set; }
        public string Gender_Name_EN { get; set; }
        public ICollection<Category_Gender> category_Genders { get; set; }
    }
}