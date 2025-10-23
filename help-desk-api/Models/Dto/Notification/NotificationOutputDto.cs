using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Notification
{
    public class NotificationOutputDto
    {
        public NotificationEvent Event { get; set; }
        public EnumNotificationType NotificationType { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public bool IsEnabled { get; set; }
        public string[] Variables { get; set; }
    }
}
