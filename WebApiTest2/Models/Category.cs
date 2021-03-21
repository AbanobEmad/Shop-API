using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }
        public string Category_Name_AR { get; set; }
        public string Category_Name_EN { get; set; }
        public string Category_Image_Path { get; set; }
        public bool Show_Home { get; set; }
        public ICollection<Category_Gender> category_Genders { get; set; }
        public ICollection<Product> products { get; set; }
        public ICollection<CategorySize> categorySizes { get; set; }

    }
}