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
    public class ChangePasswordInputDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }

    public class ForgotPasswordInputDto
    {

        [Required]
        public string UserName { get; set; }
        [JsonIgnore]
        public Tuple<ClientInfo, IPAddress> Browser { get; set; }
    }

    public class RecoverPasswordVerificationInputDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string UserToken { get; set; }
        [JsonIgnore]
        public Tuple<ClientInfo, IPAddress> Browser { get; set; }
    }
}