using Data.Interfaces;
using System;

namespace Services.Domain
{
    public record TenantListViewModel : IListViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}
