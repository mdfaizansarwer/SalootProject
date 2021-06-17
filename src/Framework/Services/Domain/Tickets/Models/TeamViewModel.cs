using Data.Interfaces;

namespace Services.Domain
{
    public record TicketTypeViewModel : IViewModel
    {
        public int Id { get; init; }

        public string Type { get; init; }
    }
}