using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NickNameCheck;

namespace NickNameCheck.Controllers
{
    public class UsersController : ApiController
    {
        private UserDB db = new UserDB();

        // GET: api/Users  
        [HttpGet]
        [Route("api/Users/")]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [HttpGet]
        [Route("api/Users/")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Users/")]
        public async Task<IHttpActionResult> NickNameAvailable(string nickname)
        {

            var nicknameIsInDB = await db.Users.AnyAsync(b => nickname.ToLower() == b.NickName.ToLower());

            return Json(!nicknameIsInDB);
        }

        // PUT: api/Users/5
        [HttpPut]
        [Route("api/Users/")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Route("api/Users")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Ok(new User { Id = user.Id, Name = user.Name, NickName = user.NickName });
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}