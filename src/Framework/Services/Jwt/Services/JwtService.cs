using Core.Setting;
using Core.Utilities;
using Data.DataProviders;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Jwt
{
    public class JwtService
    {
        #region Fields

        private readonly ApplicationSettings _applicationSettings;

        private readonly SignInManager<User> _signInManager;

        private readonly ITenantDataProvider _tenantDataProvider;

        #endregion Fields

        #region Ctor

        public JwtService(IOptionsSnapshot<ApplicationSettings> settings, SignInManager<User> signInManager, ITenantDataProvider tenantDataProvider)
        {
            _applicationSettings = settings.Value;
            _signInManager = signInManager;
            _tenantDataProvider = tenantDataProvider;
        }

        #endregion Ctor

        #region Methods

        public async Task<Token> GenerateTokenAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.SecretKey); // longer than 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.EncryptKey); // Must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await GetClaimsAsync(user);

            var tokenOptions = new SecurityTokenDescriptor
            {
                Issuer = _applicationSettings.JwtSetting.Issuer,
                Audience = _applicationSettings.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_applicationSettings.JwtSetting.NotBeforeMinutes),
                Expires = DateTime.Now.AddDays(_applicationSettings.JwtSetting.AccessTokenExpirationDays),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenOptions);
            var refreshToken = new RefreshToken()
            {
                refresh_token = GenerateRefreshToken(),
                refresh_token_expires_in = DateTime.Now.AddDays(_applicationSettings.JwtSetting.RefreshTokenExpirationDays)
            };

            return new Token(securityToken, refreshToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            var role = await _signInManager.UserManager.GetRolesAsync(user);
            //var tenant = await _tenantDataProvider.GetTenantByUserAsync(user.Id, default);

            // Add custom claims
            var claims = new List<Claim>(result.Claims)
            {
                new Claim(ClaimTypes.Name, user.UserName),
                //new Claim(ApplicationStaticVariables.TenantId, user.Team.TenantId.ToString()), todo : Tenant
                //new Claim(ClaimTypes.Role, role.FirstOrDefault()), ToDo : InvitationLink
                new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Gender, user.Gender.ToDisplay()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber??""),
            };

            return claims;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secretKey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.SecretKey); // longer than 16 character
            var encryptionkey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.EncryptKey); // Must be 16 character

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
                ValidAudience = _applicationSettings.JwtSetting.Audience,
                ValidateIssuer = true,
                ValidIssuer = _applicationSettings.JwtSetting.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateLifetime = false, // Here we are saying that we don't care about the token's expiration date
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        #endregion Methods
    }
}