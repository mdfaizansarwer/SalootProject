using Services.Jwt;

namespace Services.Domain
{
    public record UserSignInViewModel
    {
        public UserViewModel UserViewModel { get; init; }

        public Token Token { get; init; }
    }
}
