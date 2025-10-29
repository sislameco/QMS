using System;

namespace Models.Dto.Auth
{
    public class HelpDeskLoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; }
        public bool IsPasswordChange { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
