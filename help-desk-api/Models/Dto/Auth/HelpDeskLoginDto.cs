using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using UAParser;

namespace Models.Dto.Auth
{
    public class HelpDeskLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public Tuple<ClientInfo, IPAddress> Browser { get; set; }
    }
}