
namespace Models.Dto.Dashboard
{
    public class DashboardResponseDto
    {
        public DashboardFiltersDto Filters { get; set; }
        public DashboardStatsDto Stats { get; set; }
        public List<RecentActivityDto> RecentActivities { get; set; }
        public List<SlaMetricDto> SlaMetrics { get; set; }
        public List<DepartmentWorkloadDto> DeptWorkload { get; set; }
        public List<DepartmentPerformanceDto> DeptPerformance { get; set; }
        public List<DepartmentTrendDto> DeptTrends { get; set; }
    }

    public class DashboardFiltersDto
    {
        public string Role { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
    }

    public class DashboardStatsDto
    {
        public int OpenTickets { get; set; }
        public int TicketsOverdue { get; set; }
        public int TicketsDelta { get; set; }
        public int ActiveComplaints { get; set; }
        public int ComplaintsEscalated { get; set; }
        public int OpenCapas { get; set; }
        public int CapasDueThisWeek { get; set; }
        public int CapasDelta { get; set; }
        public int HighRisks { get; set; }
        public int RisksNeedMitigation { get; set; }
        public int NewRisks { get; set; }
    }

    public class RecentActivityDto
    {
        public string Code { get; set; }
        public string Tag { get; set; }
        public string TagColor { get; set; }
        public string Title { get; set; }
        public string Assignee { get; set; }
        public string Due { get; set; } // ISO string
        public string Status { get; set; }
    }

    public class SlaMetricDto
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public class DepartmentWorkloadDto
    {
        public string Name { get; set; }
        public int OpenTickets { get; set; }
        public int ActiveCapas { get; set; }
        public int HighRisks { get; set; }
    }

    public class DepartmentPerformanceDto
    {
        public string Name { get; set; }
        public int TicketResolution { get; set; }
        public int ComplaintResponse { get; set; }
        public int CapaCompletion { get; set; }
    }

    public class DepartmentTrendDto
    {
        public string Month { get; set; }
        public int Tickets { get; set; }
        public int Complaints { get; set; }
        public int Risks { get; set; }
    }

}
