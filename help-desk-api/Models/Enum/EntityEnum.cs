
using System.ComponentModel;

namespace Models.Enum
{
    public enum TicketCategory { Ticket, CAPA, Goals, Complaints }
    public enum TicketStatus { Open, InProgress, Resolved, Closed }
    public enum TicketPriority { Highest, High, Meduim, Low }
    public enum NotificationType { Email, SMS, App, System }
    public enum NotificationScheduleStatus { Pending, Processing, Sent, Failed, Cancelled }
}