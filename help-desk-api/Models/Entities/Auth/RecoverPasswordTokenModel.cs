using Models.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.Auth
{
    [Table("RecoverPasswordTokens", Schema = "auth")]
    public class RecoverPasswordTokenModel : BaseEntity<int>
    {
        public int FkUserId { get; set; }
        [ForeignKey("FkUserId")]
        public UserModel User { get; set; }
        public string OtpCode { get; set; }
        public string UserToken { get; set; }
        public bool IsVarified { get; set; }
    }
}
