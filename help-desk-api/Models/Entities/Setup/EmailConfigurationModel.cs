using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Setup
{
    [Table("EmailConfiguration", Schema = "setup")]
    public class EmailConfigurationModel : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int SMTPPort { get; set; }
        public int IMAPPort { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BCC { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string ReplyTo { get; set; }
        public NotificationEvent Event { get; set; }
    }
}