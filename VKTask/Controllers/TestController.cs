using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using VKTask.DAL;
using VKTask.DAL.Interfaces;
using VKTask.Domain.Models;

namespace VKTask.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepo _users;
        public TestController(ApplicationDbContext db, IUserRepo repo)
        {
            _db = db;
            _users = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var groups = _db.UserGroups.ToList();
            var states = _db.UserStates.ToList();
            var users = await _users.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id = 0)
        {
            var userName = User.Claims;
            var Id = userName.First(f => f.Type == "Id").Value;
            var user = await _users.GetByIdAsync(Guid.Parse(Id));
            if (user.UserGroup.Description != "Admin")
                return Forbid();
            return Ok("Auth");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto login)
        {
            if (Request.Cookies.ContainsKey("userId"))
            {
                return BadRequest("Logged in");
            }
            var users = await _users.GetUsers();
            var user = users.FirstOrDefault(x => x.Login == login.Login && x.Password == login.Password);
            if (user is null)
            {
                return BadRequest("Incorrect data");
            }
            else
            {
                var CookieOptions = new CookieOptions() {Expires = DateTime.Now.AddMinutes(3) };
                Response.Cookies.Append("userId", user.Id.ToString(), CookieOptions);
                return Ok();
            }
        }

        [HttpGet]
        [Route("Auth")]
        public ActionResult GetSmth()
        {
            if (!Request.Cookies.ContainsKey("userId"))
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost]
        [Route("Logout")]
        public ActionResult Logout()
        {
            if (!Request.Cookies.ContainsKey("userId"))
            {
                return Unauthorized();
            }
            else
            {
                Response.Cookies.Delete("userId");
                return Ok();
            }
        }

        [HttpPost]
        [Route("Login")]
        //[Authorize]
        public async Task<ActionResult> TestLogin(LoginDto login)
        {
            var loginString = login.Login.ToString() + ":" + login.Password;
            var users = await _users.GetUsers();
            var user = users.FirstOrDefault(x => x.Login == login.Login && x.Password == login.Password);
            Request.Headers.Authorization.Append(loginString);
            var CookieOptions = new CookieOptions() { Expires = DateTime.Now.AddMinutes(3) };
            Response.Cookies.Append("userId", user.Id.ToString(), CookieOptions);
            return Ok();
        }
    }
}

