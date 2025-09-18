using System;

namespace Models.Dto.Auth
{
    public class HelpDeskLoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public long UserId { get; set; }
    }
}
