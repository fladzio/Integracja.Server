﻿namespace Integracja.Server.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationTime { get; set; }
    }
}
