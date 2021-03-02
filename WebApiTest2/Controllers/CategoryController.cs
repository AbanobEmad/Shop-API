using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db=new ApplicationDbContext() ;
        [Route("Category")]
        public IHttpActionResult GetCategory()
        {
            return Ok(db.Categories);
        }
        [Route("Category")]
        public IHttpActionResult PostCategory(Category category)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return Created("",category.ID);
        }
    }
}
