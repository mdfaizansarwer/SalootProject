using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.Midllewares;
using Services.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalootProject.Api.Controllers
{
    public class RolesController : BaseController
    {
        #region Fields

        private readonly RoleService _roleService;

        #endregion Fields

        #region Ctor

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion Ctor

        #region Actions

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<RoleListViewModel>>> GetAllRoles()
        {
            var roleList = await _roleService.GetAllRolesAsync();
            return Ok(roleList);
        }

        [HttpGet("{id:int:min(1)}"), Authorize(Roles = "Admin")]
        public async Task<ApiResponse<RoleViewModel>> GetRoleById(int id)
        {
            var roleDto = await _roleService.GetRoleByIdAsync(id);
            return Ok(roleDto);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ApiResponse<RoleViewModel>> CreateRole(RoleCreateUpdateViewModel roleCreateOrUpdateViewModel)
        {
            var roleViewModel = await _roleService.CreateAsync(roleCreateOrUpdateViewModel);
            return Ok(roleViewModel);
        }

        [HttpPut("{id:int:min(1)}"), Authorize]
        public async Task<ApiResponse> UpdateRole(int id, RoleCreateUpdateViewModel roleCreateOrUpdateViewModel)
        {
            await _roleService.UpdateAsync(id, roleCreateOrUpdateViewModel);
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}"), Authorize]
        public async Task<ApiResponse> DeleteRole(int id)
        {
            await _roleService.DeleteAsync(id);
            return Ok();
        }

        #endregion Actions
    }
}