using Models.Dto.Pagination;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Notification
{
    public class NotificationPagination: PageBase
    {
        public EnumNotificationType NotificationType { get; set; }
    }
}
