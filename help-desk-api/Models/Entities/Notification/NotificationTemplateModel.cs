using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Notification
{
    [Table("NotificationTemplate", Schema = "notification")]
    public class NotificationTemplateModel : BaseEntity<long>
    {
        public long TicketTypeId { get; set; }
        public NotificationTrigger Trigger { get; set; }
        public NotificationType NotificationType { get; set; }
        public long? EmailConfigurationId { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string CcList { get; set; }
        public bool IsEnabled { get; set; }
    }
}