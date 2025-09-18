using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AppSettings
{
    public class AppSettings
    {
        public static string Env { get; set; }
        public static Redis Redis { get; set; }
        public static ELKConfiguration ELKConfiguration { get; set; }
        public static List<string> CorsOrigins { get; set; }
        public static JWT Jwt { get; set; }
        public static BasicAuthCredential BasicAuthCredential { get; set; }
    }
    public class Redis
    {
        public string ConnectionString { get; set; }
    }
    public class ELKConfiguration
    {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class JWT
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiryMinutes { get; set; }
    }
    public class BasicAuthCredential
    {
        public string AccessKey { get; set; }
        public string AccessSecret { get; set; }
    }

}
