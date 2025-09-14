namespace QToken_Native.API
{
    public static class APIHost
    {
        public static string Host => GetHost();
        private static string GetHost()
        {
#if ANDROID
        return "http://10.0.2.2:5000"; // Android emulator
#elif WINDOWS
            return "https://localhost:5001"; // Windows MAUI
#elif IOS
        return "http://localhost:5000"; // iOS simulator
#else
        return "https://your-production-url.com"; // fallback or production
#endif
        }
    }
    public static class APIEndpoints
    {
        public const string Register = "/api/user/register";
        public const string Login = "/api/user/login";
        public const string GetSpecialities = "/api/specialties";
        public const string HaveAdmin = "/api/user/haveadmin";
        public const string IsAlive = "/qtoken";
    }
}
