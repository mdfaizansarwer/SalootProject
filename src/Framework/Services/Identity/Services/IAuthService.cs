using Services.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Domain
{
    public interface IAuthService
    {
        Task<UserSignInViewModel> SignInAsync(TokenRequest tokenRequest);

        Task<UserSignInViewModel> RegisterAsync(UserCreateViewModel userDto);

        Task LogoutAsync(ClaimsPrincipal claimsPrincipal);

        Task<Token> RefreshTokenAsync(TokenRequest tokenRequest);
    }
}