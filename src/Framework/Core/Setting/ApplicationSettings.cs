namespace Core.Setting
{
    public sealed record ApplicationSettings
    {
        public JwtSetting JwtSetting { get; set; }

        public IdentitySetting IdentitySetting { get; set; }

        public DatabaseSetting DatabaseSetting { get; set; }

        public LogSetting LogSetting { get; set; }

        public MailSetting MailSetting { get; set; }
    }
}