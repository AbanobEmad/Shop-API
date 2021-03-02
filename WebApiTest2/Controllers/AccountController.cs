using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class AccountController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        
        public async Task <IHttpActionResult> register(UserModel account)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(new ApplicationDbContext());
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
                IdentityUser user = new IdentityUser();
                user.UserName = account.Phone;
                user.PasswordHash = account.Password;
                IdentityResult result=await manager.CreateAsync(user, account.Password);
                if(result.Succeeded)
                {
                    Models.DataUser dataUser= new DataUser();
                    dataUser.Name = account.Name;
                    dataUser.Cash = 0;
                    dataUser.point = 0;
                    dataUser.User_ID = user.Id;
                    db.DataUsers.Add(dataUser);
                    db.SaveChanges();
                    Models.Result_Message result_ = new Result_Message();
                    result_.Status = true;
                    result_.Message = "Done Register";
                    return Created("", result_);
                }
                else
                {
                    string ErrorMessage = "";
                    foreach(var e in result.Errors)
                    {
                        ErrorMessage += e;
                    }
                    return BadRequest(ErrorMessage);
                }

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [Route("User")]
        public async Task <IHttpActionResult> GetUser()
        {
            try
            {
                string user_id = User.Identity.GetUserId();
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(new ApplicationDbContext());
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
                IdentityUser user = await manager.FindByIdAsync(user_id);
                Models.DataUser dataUser = db.DataUsers.FirstOrDefault(D => D.User_ID == user_id);
                if(user==null||dataUser==null)
                {
                    return NotFound();
                }
                Models.UserModel userModel = new UserModel();
                userModel.Phone = user.UserName;
                userModel.Name = dataUser.Name;
                userModel.Cash = dataUser.Cash;
                userModel.point = dataUser.point;
                return Ok(userModel);

            }
            catch(Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}
