using System.ComponentModel;

namespace Models.Enum
{
    /// <summary>
    /// Category of the ticket.
    /// </summary>
    public enum EnumQMSType { Ticket = 1, CAPA = 2, Goals = 3, Complaints = 4 }

    /// <summary>
    /// Status of the ticket.
    /// </summary>
    public enum EnumTicketStatus { Open = 1, InProgress = 2, Resolved = 3, Closed = 4 }

    /// <summary>
    /// Priority of the ticket.
    /// </summary>
    public enum EnumPriority { Highest = 1, High = 2, Medium = 3, Low = 4 }

    /// <summary>
    /// Type of notification channel.
    /// </summary>
    public enum EnumNotificationType { Email = 1, SMS = 2, App = 3, System = 4 }

    /// <summary>
    /// Status of the notification schedule.
    /// </summary>
    public enum NotificationScheduleStatus { Pending = 1, Processing = 2, Sent = 3, Failed = 4, Cancelled = 5 }

    /// <summary>
    /// Trigger event for notification.
    /// </summary>
    public enum NotificationEvent { Created = 1, Updated = 2, Resolved = 3, Closed = 4, SLADue = 5, SLAOverdue = 6, RecoveryPassword = 7, UserInvitation = 8}

    /// <summary>
    /// Type of audit action performed.
    /// </summary>
    public enum AuditActionType { Created = 1, Updated = 2, Deleted = 3, Viewed = 4, Restored = 5, StatusChanged = 6 }

    public enum EnumRStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
    /// <summary>
    /// Time unit used for scheduling, due calculation.
    /// </summary>
    public enum EnumUnit
    {
        Minutes = 1,
        Hours = 2,
        Days = 3,
        Weeks = 4,
        Months = 5
    }
    public enum EnumCrud
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4
    }
    public enum  FileType
    {
        pu
    }
}