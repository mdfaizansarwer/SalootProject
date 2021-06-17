using Data.Interfaces;
using FluentValidation;

namespace Services.Domain
{
    public record TicketTypeCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
    {
        public string Type { get; init; }
    }

    public class TicketTypeCreateUpdateViewModelValidator : AbstractValidator<TicketTypeCreateUpdateViewModel>
    {
        public TicketTypeCreateUpdateViewModelValidator()
        {
            RuleFor(p => p.Type).NotEmpty().MaximumLength(30);
        }
    }
}