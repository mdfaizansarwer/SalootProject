using Data.Interfaces;
using System;

namespace Services.Domain
{
    public record TeamListViewModel : IListViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public int? ParentId { get; init; }

        public int? TenantId { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}