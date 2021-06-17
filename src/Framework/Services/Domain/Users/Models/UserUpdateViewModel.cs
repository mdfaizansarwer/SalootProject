using Core.Enums;
using Data.Interfaces;
using FluentValidation;
using System;

namespace Services.Domain
{
    public record UserUpdateViewModel : IUpdateViewModel
    {
        public string Username { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public string Email { get; init; }

        public DateTime Birthdate { get; init; }

        public string PhoneNumber { get; init; }

        public GenderType Gender { get; init; }

        public int? ProfilePictureId { get; set; }

        public int? TeamId { get; init; }
    }

    public class UserUpdateViewModelValidator : AbstractValidator<UserUpdateViewModel>
    {
        public UserUpdateViewModelValidator()
        {
            RuleFor(p => p.Username).NotEmpty().MaximumLength(15);

            RuleFor(p => p.Firstname).NotEmpty().MaximumLength(35);

            RuleFor(p => p.Lastname).NotEmpty().MaximumLength(35);

            RuleFor(p => p.Email).NotEmpty().EmailAddress().MaximumLength(320);

            RuleFor(p => p.PhoneNumber).MaximumLength(15).Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            RuleFor(p => p.Gender).IsInEnum();

            When(p => p.TeamId.HasValue, () => RuleFor(p => p.TeamId).GreaterThanOrEqualTo(1));
        }
    }
}