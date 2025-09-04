
using System.ComponentModel;

namespace Models.Enum
{
    public enum BookingStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }

    public enum PaymentStatus
    {
        Unpaid,
        Paid,
        Failed
    }

    public enum EventType
    {
        BirthDayParty,
        SportsFirness,
        MusicDanceTheater,
        CompanyMeeting,
        Other
    }

    public enum TimeSegmentType
    {
        [Description("06:00 - 12:00")]
        Morning,

        [Description("12:00 - 17:00")]
        Afternoon,

        [Description("17:00 - 21:00")]
        Evening,

        [Description("21:00 - 06:00")]
         Night
    }

    public enum AgeGroup
    {
        [Description("U17")]
        Children,
        [Description("18+")]
        Adult,
        [Description("Mixed")]
        Both
    }
}