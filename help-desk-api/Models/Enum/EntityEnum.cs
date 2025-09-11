using System.ComponentModel;

namespace Models.Enum
{
    /// <summary>
    /// Category of the ticket.
    /// </summary>
    public enum TicketCategory { Ticket = 1, CAPA = 2, Goals = 3, Complaints = 4 }

    /// <summary>
    /// Status of the ticket.
    /// </summary>
    public enum TicketStatus { Open = 1, InProgress = 2, Resolved = 3, Closed = 4 }

    /// <summary>
    /// Priority of the ticket.
    /// </summary>
    public enum TicketPriority { Highest = 1, High = 2, Medium = 3, Low = 4 }

    /// <summary>
    /// Type of notification channel.
    /// </summary>
    public enum NotificationType { Email = 1, SMS = 2, App = 3, System = 4 }

    /// <summary>
    /// Status of the notification schedule.
    /// </summary>
    public enum NotificationScheduleStatus { Pending = 1, Processing = 2, Sent = 3, Failed = 4, Cancelled = 5 }

    /// <summary>
    /// Trigger event for notification.
    /// </summary>
    public enum NotificationTrigger { Created = 1, Updated = 2, Resolved = 3, Closed = 4, SLADue = 5, SLAOverdue = 6 }

    /// <summary>
    /// Type of audit action performed.
    /// </summary>
    public enum AuditActionType { Created = 1, Updated = 2, Deleted = 3, Viewed = 4, Restored = 5, StatusChanged = 6 }

    public enum EnumEntityStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
}