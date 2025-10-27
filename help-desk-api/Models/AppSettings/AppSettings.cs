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
        public static ApiConf QsmartAPI { get; set; }
        public static ApiConf HRAPI { get; set; }
        public static string TemporaryFilePath { get; set; }
        public static string TicketPath { get; set; }
        public static Slack Slack { get; set; }
        public static ApiConf QMSApi { get; set; }
        public static JobScheduler JobScheduler { get; set; }

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
    public class ApiConf
    {
        public string AuthorizationKey { get; set; }
        public string Company { get; set; }
        public string BaseUrl { get; set; }
    }
    public class Slack
    {
        public string WebhookUrl { get; set; }
        public string BotToken { get; set; }
        public string EndPoint { get; set; }
        public string BaseUrl { get; set; }

    }
    public class JobScheduler
    {
        public string AuthorizationKey { get; set; }
        public string BaseUrl { get; set; }
    }

}
