namespace TicketManagement.Common.JwtTokenAuth.Settings
{
    public class JwtTokenSettings
    {
        public string JwtIssuer { get; set; } = "MySecretIssuer";

        public string JwtAudience { get; set; } = "MySecretAudience";

        public string JwtSecretKey { get; set; } = "9r#cExiS3ivug_=r";
    }
}
