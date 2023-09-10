namespace despesas_backend_api_net_core.Infrastructure.Security.Configuration
{
    public class TokenConfiguration
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public int Seconds { get;  set; }
    }
}
