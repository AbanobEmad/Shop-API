using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class GenderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("Gender")]
        public IHttpActionResult GetGender()
        {
            return Ok(db.Genders);
        }
        [Route("Gender")]
        public IHttpActionResult PostGender(Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Genders.Add(gender);
            db.SaveChanges();
            return Created("", gender.ID);
        }
    }
}
