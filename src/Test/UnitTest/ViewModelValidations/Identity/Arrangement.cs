using Core.Enums;
using Services.Domain;
using System;

namespace UnitTest.ViewModelValidations.Identity
{
    public class Arrangement
    {
        public UserCreateViewModel ValidUserCreateViewModel => new()
        {
            Username = "Admin",
            Firstname = "Brad",
            Lastname = "Pitt",
            Email = "BradPitt@gmail.com",
            Password = "123456",
            Birthdate = DateTime.Now,
            PhoneNumber = "(281)388-0388",
            Gender = GenderType.Male,
            TeamId = 1,
            IsActive = true
        };

        public UserUpdateViewModel ValidUserUpdateViewModel => new()
        {
            Username = "Admin",
            Firstname = "Brad",
            Lastname = "Pitt",
            Email = "BradPitt@gmail.com",
            Birthdate = DateTime.Now,
            PhoneNumber = "(281)388-0388",
            Gender = GenderType.Male,
            TeamId = 1,
        };

        public TokenRequest ValidTokenRequest => new()
        {
            GrantType = "password",
            Username = "Admin",
            Password = "123456",
            AccessToken = "",
            RefreshToken = ""
        };

        public RoleCreateUpdateViewModel ValidRoleCreateUpdateViewModel => new()
        {
            Name = "Admin",
            Description = "This is Admin Role"
        };
    }
}