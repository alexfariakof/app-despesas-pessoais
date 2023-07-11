namespace despesas_backend_api_net_core.Domain.Entities
{
    public class AuthResponse
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }        
    }
}
