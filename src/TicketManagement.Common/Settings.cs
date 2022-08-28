namespace TicketManagement.Common;

public static class Settings
{
    public struct Jwt
    {
        public Jwt()
        {
        }

        public static string JwtOrCookieScheme { get; } = "jwt_or_cookie";
        public static string JwtIssuer { get; } = "MySecretIssuer";
        public static string JwtAudience { get; } = "MySecretAudience";
        public static string JwtSecretKey { get; } = "9r#cExiS3ivug_=r";
    }
}
