using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;
using VKTask.Response;
using VKTask.Service.Interfaces;

namespace VKTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService users, ILogger<UserController> logger)
        {
            _logger = logger;
            _users = users;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync(CreateUserDto userModel)
        {
            BaseResponse<User> response = new BaseResponse<User>();
            try
            {
                response.Content = await _users.CreateUserAsync(userModel);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.IsSuccess = false;
            }

            return (response.IsSuccess) ? Ok(response.Content) : BadRequest();            
        }
    }
}
