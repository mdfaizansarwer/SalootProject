using Core.Enums;

namespace Core.Setting
{
    public sealed record DatabaseSetting
    {
        public ConnectionStrings ConnectionStrings { get; init; }

        public bool StoreFilesOnDatabase { get; init; }

        public DatabaseProvider DatabaseProvider { get; init; }
    }
}
