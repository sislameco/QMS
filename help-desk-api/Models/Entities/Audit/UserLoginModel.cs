using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Audit
{
    [Table("UserLogins", Schema = "log")]
    public class UserLoginModel
    {
        [Key]
        public long Id { get; set; }
        public int FkUserId { get; set; }
        [MaxLength(200)]
        public string IpAddress { get; set; }
        [MaxLength(250)]
        public string Browser { get; set; }
        [MaxLength(250)]
        public string MachineUser { get; set; }
        public DateTime LoginTime { get; set; }
    }

    [Table("RefreshTokens", Schema = "log")]
    public class RefreshTokenModel
    {
        [Key]
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public long FkUserId { get; set; }
        public string UserIp { get; set; }
        public long FkLoginId { get; set; }
    }
}
