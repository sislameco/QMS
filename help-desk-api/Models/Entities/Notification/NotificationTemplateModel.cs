using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Notification
{
    [Table("NotificationTemplate", Schema = "notification")]
    public class NotificationTemplateModel : BaseEntity<int>
    {
        public int FkCompanyId { get; set; }
        public NotificationEvent Event { get; set; }
        public EnumNotificationType NotificationType { get; set; }
        public int? EmailConfigurationId { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string HeaderTemplate { get; set; }
        public string FooterTemplate { get; set; }
        public bool IsEnabled { get; set; }
        public string[] Variables { get; set; }
    }
}