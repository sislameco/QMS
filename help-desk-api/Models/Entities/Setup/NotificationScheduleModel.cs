using Models.Entities.Org;
using Models.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Setup
{
    [Table("NotificationSchedule", Schema = "setup")]
    public class NotificationScheduleModel : BaseEntity<long>
    {
        public long FKNotificationConfigId { get; set; }
        public int? FKTicketId { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public NotificationType NotificationType { get; set; }
        public long? FKEmailConfigurationId { get; set; }

        public DateTime ScheduledTime { get; set; }
        public DateTime? SentTime { get; set; }
        public int RetryCount { get; set; }
        public int MaxRetryCount { get; set; }
        public NotificationScheduleStatus Status { get; set; }
        public string ErrorMessage { get; set; }

        [ForeignKey("FKEmailConfigurationId")]
        public EmailConfigurationModel EmailConfiguration { get; set; }

        [ForeignKey("FKTicketId")]
        public EmailConfigurationModel EmailConfiguration { get; set; }
    }
}