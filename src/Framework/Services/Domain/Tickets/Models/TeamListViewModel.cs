using Data.Interfaces;
using System;

namespace Services.Domain
{
    public record TicketTypeListViewModel : IListViewModel
    {
        public int Id { get; init; }

        public string Type { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}