using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.Midllewares;
using Services.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalootProject.Api.Controllers
{
    public class UsersController : BaseController
    {
        #region Fields

        private readonly UserService _userService;

        #endregion Fields

        #region Ctor

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        #endregion Ctor

        #region Actions

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<UserListViewModel>>> GetAllUsers()
        {
            var userList = await _userService.GetAllUsersAsync();
            return Ok(userList);
        }

        [HttpGet("{id:int:min(1)}"), Authorize(Roles = "Admin,SystemUser")]
        public async Task<ApiResponse<UserViewModel>> GetUserById(int id)
        {
            var userViewModel = await _userService.GetByIdAsync(id);
            return Ok(userViewModel);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserViewModel>> CreateUser(UserCreateViewModel userCreateViewModel)
        {
            var userViewModel = await _userService.CreateAsync(userCreateViewModel);
            return userViewModel;
        }

        [HttpPut("{id:int:min(1)}"), Authorize]
        public async Task<ApiResponse> UpdateUser(int id, UserUpdateViewModel userUpdateViewModel)
        {
            await _userService.UpdateAsync(id, userUpdateViewModel);
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}"), Authorize]
        public async Task<ApiResponse> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

        #endregion Actions
    }
}