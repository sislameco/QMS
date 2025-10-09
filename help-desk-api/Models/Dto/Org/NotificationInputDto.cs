using Models.Entities;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Org
{
    public class NotificationInputDto
    {
        public int Id { get; set; }
        public int EmailConfigurationId { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
    }
    public class NotificationOutputDto
    {
        public int Id { get; set; }
        public NotificationEvent Event { get; set; }
        public NotificationType NotificationType { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string HeaderTemplate { get; set; }
        public string FooterTemplate { get; set; }
    }
}
