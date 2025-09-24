using Models.Dto.Dashboard;
using Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo.UserManagement
{
    public interface IDashboardRepository
    {
        public Task<DashboardResponseDto> GetDashboard();
    }
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HelpDbContext _context;

        public DashboardRepository(HelpDbContext context)
        {
            _context = context;
        }
       
        public async Task<DashboardResponseDto> GetDashboard()
        {
            var response = new DashboardResponseDto
            {
                Filters = new DashboardFiltersDto
                {
                    Role = "Quality Manager",
                    Company = "CHS",
                    Department = "All"
                },
                Stats = new DashboardStatsDto
                {
                    OpenTickets = 12,
                    TicketsOverdue = 3,
                    TicketsDelta = 2,
                    ActiveComplaints = 3,
                    ComplaintsEscalated = 1,
                    OpenCapas = 5,
                    CapasDueThisWeek = 2,
                    CapasDelta = 1,
                    HighRisks = 8,
                    RisksNeedMitigation = 3,
                    NewRisks = 1
                },
                RecentActivities = new List<RecentActivityDto>
        {
            new() { Code = "T-001", Tag = "Quality", TagColor = "primary", Title = "Equipment calibration overdue", Assignee = "John Smith", Due = "2025-08-20", Status = "overdue" },
            new() { Code = "C-025", Tag = "Customer Care", TagColor = "warning", Title = "Product quality issue reported", Assignee = "Sarah Wilson", Due = "2025-08-25", Status = "investigation" },
            new() { Code = "CAPA-012", Tag = "Quality", TagColor = "info", Title = "Supplier audit findings", Assignee = "Mike Johnson", Due = "2025-08-30", Status = "in-progress" },
            new() { Code = "R-008", Tag = "IT", TagColor = "secondary", Title = "Data security vulnerability", Assignee = "Lisa Chen", Due = "2025-09-01", Status = "open" }
        },
                SlaMetrics = new List<SlaMetricDto>
        {
            new() { Label = "Tickets Resolution", Value = 85 },
            new() { Label = "Complaint Response", Value = 92 },
            new() { Label = "CAPA Completion", Value = 78 },
            new() { Label = "Risk Mitigation", Value = 68 }
        },
                DeptWorkload = new List<DepartmentWorkloadDto>
        {
            new() { Name = "Quality Department", OpenTickets = 8, ActiveCapas = 5, HighRisks = 3 },
            new() { Name = "Customer Care", OpenTickets = 3, ActiveCapas = 0, HighRisks = 0 },
            new() { Name = "IT Department", OpenTickets = 3, ActiveCapas = 0, HighRisks = 2 }
        },
                DeptPerformance = new List<DepartmentPerformanceDto>
        {
            new() { Name = "Quality Department", TicketResolution = 82, ComplaintResponse = 88, CapaCompletion = 76 },
            new() { Name = "Customer Care", TicketResolution = 90, ComplaintResponse = 95, CapaCompletion = 0 },
            new() { Name = "IT Department", TicketResolution = 75, ComplaintResponse = 0, CapaCompletion = 60 }
        },
                DeptTrends = new List<DepartmentTrendDto>
        {
            new() { Month = "June", Tickets = 14, Complaints = 4, Risks = 2 },
            new() { Month = "July", Tickets = 12, Complaints = 3, Risks = 3 },
            new() { Month = "August", Tickets = 15, Complaints = 5, Risks = 4 }
        }
            };

            return await Task.FromResult(response);
        }

    }
}
