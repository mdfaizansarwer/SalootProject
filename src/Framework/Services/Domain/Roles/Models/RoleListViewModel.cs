using Data.Interfaces;

namespace Services.Domain
{
    public record RoleListViewModel : IListViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string NormalizedName { get; init; }
    }
}