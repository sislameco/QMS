using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Audit
{
    [Table("UserLogins", Schema = "log")]
    public class UserLoginModel:BaseEntity<long>
    {
        public int FkUserId { get; set; }
        [MaxLength(200)]
        public string IpAddress { get; set; }
        [MaxLength(250)]
        public string Browser { get; set; }
        [MaxLength(250)]
        public string MachineUser { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
