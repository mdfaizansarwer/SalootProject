using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.Midllewares;
using Services.Domain;
using Services.Jwt;
using System.Threading.Tasks;

namespace SalootProject.Api.Controllers
{
    public class AuthorizationController : BaseController
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion Fields

        #region Ctor

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion Ctor

        #region Action

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<ApiResponse<UserSignInViewModel>> Login(TokenRequest tokenRequest)
        {
            var token = await _authService.SignInAsync(tokenRequest);
            return Ok(token);
        }

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<ApiResponse<UserSignInViewModel>> Register(UserCreateViewModel userDto)
        {
            return Ok(await _authService.RegisterAsync(userDto));
        }

        [HttpPost("[action]"), Authorize]
        public async Task<ApiResponse> Logout()
        {
            await _authService.LogoutAsync(User);
            return Ok();
        }

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<ApiResponse<Token>> RefreshToken(TokenRequest tokenRequest)
        {
            var token = await _authService.RefreshTokenAsync(tokenRequest);
            return Ok(token);
        }

        #endregion Action
    }
}