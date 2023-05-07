using Microsoft.AspNetCore.Authorization;
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
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            BaseResponse<User> response = new BaseResponse<User>();
            try
            {
                response.Content = await _users.CreateUserAsync(userModel, cts);
            }
            catch(OperationCanceledException)
            {
                _logger.Log(LogLevel.Error, "5 seconds time limit is over");
                response.IsSuccess = false;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.IsSuccess = false;
            }

            return (response.IsSuccess) ? Ok(response.Content) : BadRequest();            
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserAsync([FromRoute]string id)
        {
            BaseResponse<User> response = new BaseResponse<User>();
            try
            {
                var userId = User.Claims.First(i => i.Type == "Id").Value;
                response.Content = await _users.DeleteAsync(userId, Guid.Parse(id));
            }

            catch (HttpRequestException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.IsSuccess = false;
                return Forbid();
            }
            return (response.IsSuccess) ? Ok(response.Content) : BadRequest();
        }

        [HttpGet]
        [Route("GetUsers/{page}")]
        public async Task<ActionResult<User[]>> GetAsync([FromRoute]int page = 0)
        {
            var response = new BaseResponse<User[]>();
            try
            {
                response.Content = await _users.GetAsync(page);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.IsSuccess = false;
            }
            return (response.IsSuccess) ? Ok(response.Content) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByIdAsync([FromRoute]string id)
        {
            var response = new BaseResponse<User>();
            try
            {
                response.Content = await _users.GetByIdAsync(Guid.Parse(id));
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
