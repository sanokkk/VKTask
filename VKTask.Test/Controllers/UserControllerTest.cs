using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using VKTask.Domain.Dtos;
using VKTask.Domain.Models;

namespace VKTask.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly HttpClient _client;
        public UserControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
            
        }
        [Fact]
        public async void UserController_GetUsers_ReturnOK_ReturnUserArray()
        {
            //Arrange
            var response = await _client.GetAsync("/api/User/GetUsers/0");

            //Act
            var result = await response.Content.ReadFromJsonAsync<User[]>();
            var statusCode = response.StatusCode;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK,statusCode);
            Assert.Equal(typeof(User[]), result.GetType());
        }

        [Fact]
        public async void UserController_GetUserById_ReturnOK_ReturnUser()
        {
            //Arrange
            var response = await _client.GetAsync("/api/User/7f9e78ad-7763-44fb-9b3a-28fc62e1c7db");

            //Act
            var result = await response.Content.ReadFromJsonAsync<User>();
            var statusCode = response.StatusCode;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, statusCode);
            Assert.Equal(typeof(User), result.GetType());
        }

        [Fact]
        public async void UserController_CreateUser_ReturnCreated_ReturnUser()
        {
            //Arrange
            var model = new CreateUserDto() { Login = "newUser", Password = "newPass", UserGroupId = 2 };
            var response = await _client.PostAsJsonAsync("api/User", model);

            //Act
            var result = await response.Content.ReadFromJsonAsync<User>();
            var statusCode = response.StatusCode;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, statusCode);
            Assert.Equal(typeof(User), result.GetType());
        }

        [Fact]
        public async void UserController_DeleteUser_ReturnOk_ReturnUser()
        {
            //Arrange
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("basic", "c2Fub2trazoyMDAyMDkxOA==");
            var response = await _client.DeleteAsync("api/User/852aedd7-0ef3-4399-b1ee-e2f3ad9a09c6");

            //Act
            var result = await response.Content.ReadFromJsonAsync<User>();
            var statusCode = response.StatusCode;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, statusCode);
            Assert.Equal(typeof(User), result.GetType());
            Assert.Equal(2, result.UserStateId);
        }
    }
}
