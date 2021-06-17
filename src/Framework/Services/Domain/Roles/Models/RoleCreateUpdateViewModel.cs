using Data.Interfaces;
using FluentValidation;

namespace Services.Domain
{
    public record RoleCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }
    }

    public class RoleCreateUpdateViewModelValidator : AbstractValidator<RoleCreateUpdateViewModel>
    {
        public RoleCreateUpdateViewModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(15);

            RuleFor(p => p.Description).NotEmpty().MaximumLength(100);
        }
    }
}