using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VKTask.DAL;

namespace VKTask.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TestController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get([FromRoute]int id = 0)
        {
            return Ok("Auth");
        }
    }
}
