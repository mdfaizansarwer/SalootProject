namespace Core.Setting
{
    public sealed record ConnectionStrings
    {
        public string SqlServer { get; init; }

        public string Postgres { get; init; }
    }
}
