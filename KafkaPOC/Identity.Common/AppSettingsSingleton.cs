namespace Identity.Common
{
    public class AppSettingsSingleton
    {
        public int tokenExpirationInMinutes { get; set; }
        public bool UseStrongPassword { get; set; }
    }
}
