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
        public static ReidsCon Redis { get; set; }
        public static ELKConfiguration ELKConfiguration { get; set; }
        public static List<string> CorsOrigins { get; set; }
    }
    public class ReidsCon
    {
        public string ConnectionString { get; set; }
    }
    public class ELKConfiguration
    {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
