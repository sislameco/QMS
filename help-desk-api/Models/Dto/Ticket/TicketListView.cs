using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Ticket
{
    using System;
    using System.Collections.Generic;

    namespace Models.Dto.Tickets
    {
        public class TicketListView
        {
            public int Id { get; set; }
            public string TicketNumber { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Priority { get; set; } = string.Empty;
            public string Assignee { get; set; } = string.Empty;
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
            public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
        }

        public static class TicketSeed
        {
            public static List<TicketListView> GetTickets()
            {
                return new List<TicketListView>
            {
                new TicketListView { Id = 1, TicketNumber = "TKT-2025-001", Subject = "Customer Complaint", Title = "Product damaged on delivery", Description = "Customer reported broken product", Status = "Open", Priority = "High", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-30), LastUpdate = DateTime.UtcNow.AddDays(-29) },
                new TicketListView { Id = 2, TicketNumber = "TKT-2025-002", Subject = "Network Issue", Title = "VPN not working", Description = "Remote users cannot connect", Status = "In Progress", Priority = "Medium", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-29), LastUpdate = DateTime.UtcNow.AddDays(-28) },
                new TicketListView { Id = 3, TicketNumber = "TKT-2025-003", Subject = "Login Error", Title = "Invalid credentials error", Description = "Users unable to login with correct credentials", Status = "Resolved", Priority = "High", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-28), LastUpdate = DateTime.UtcNow.AddDays(-27) },
                new TicketListView { Id = 4, TicketNumber = "TKT-2025-004", Subject = "UI Bug", Title = "Button not aligned", Description = "Submit button misaligned on mobile view", Status = "Closed", Priority = "Low", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-27), LastUpdate = DateTime.UtcNow.AddDays(-26) },
                new TicketListView { Id = 5, TicketNumber = "TKT-2025-005", Subject = "Performance Issue", Title = "Dashboard very slow", Description = "Dashboard takes 15 seconds to load", Status = "Open", Priority = "High", Assignee = "Sohan", CreatedDate = DateTime.UtcNow.AddDays(-26), LastUpdate = DateTime.UtcNow.AddDays(-25) },
                new TicketListView { Id = 6, TicketNumber = "TKT-2025-006", Subject = "Email Error", Title = "SMTP not working", Description = "System not sending emails", Status = "In Progress", Priority = "Medium", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-25), LastUpdate = DateTime.UtcNow.AddDays(-24) },
                new TicketListView { Id = 7, TicketNumber = "TKT-2025-007", Subject = "Mobile Crash", Title = "App crashes on login", Description = "Android app crashes immediately after login", Status = "Open", Priority = "High", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-24), LastUpdate = DateTime.UtcNow.AddDays(-23) },
                new TicketListView { Id = 8, TicketNumber = "TKT-2025-008", Subject = "Translation Issue", Title = "French missing texts", Description = "UI not translated in French", Status = "Resolved", Priority = "Low", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-23), LastUpdate = DateTime.UtcNow.AddDays(-22) },
                new TicketListView { Id = 9, TicketNumber = "TKT-2025-009", Subject = "Data Sync Error", Title = "Sync fails for large dataset", Description = "Data sync not working for >5000 rows", Status = "In Progress", Priority = "High", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-22), LastUpdate = DateTime.UtcNow.AddDays(-21) },
                new TicketListView { Id = 10, TicketNumber = "TKT-2025-010", Subject = "Invoice Error", Title = "Tax calculation wrong", Description = "Invoice showing incorrect VAT", Status = "Open", Priority = "High", Assignee = "Sohan", CreatedDate = DateTime.UtcNow.AddDays(-21), LastUpdate = DateTime.UtcNow.AddDays(-20) },
                new TicketListView { Id = 11, TicketNumber = "TKT-2025-011", Subject = "Report Error", Title = "PDF not generating", Description = "Export report as PDF not working", Status = "Closed", Priority = "Medium", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-20), LastUpdate = DateTime.UtcNow.AddDays(-19) },
                new TicketListView { Id = 12, TicketNumber = "TKT-2025-012", Subject = "Access Issue", Title = "User cannot login", Description = "Active user blocked from login", Status = "Open", Priority = "High", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-19), LastUpdate = DateTime.UtcNow.AddDays(-18) },
                new TicketListView { Id = 13, TicketNumber = "TKT-2025-013", Subject = "Notification Delay", Title = "Push notification late", Description = "Notifications received after 30 minutes", Status = "In Progress", Priority = "Low", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-18), LastUpdate = DateTime.UtcNow.AddDays(-17) },
                new TicketListView { Id = 14, TicketNumber = "TKT-2025-014", Subject = "Broken Link", Title = "Help page not opening", Description = "Broken link in documentation", Status = "Resolved", Priority = "Low", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-17), LastUpdate = DateTime.UtcNow.AddDays(-16) },
                new TicketListView { Id = 15, TicketNumber = "TKT-2025-015", Subject = "DB Timeout", Title = "Query very slow", Description = "Reports query timing out", Status = "Open", Priority = "High", Assignee = "Sohan", CreatedDate = DateTime.UtcNow.AddDays(-16), LastUpdate = DateTime.UtcNow.AddDays(-15) },
                new TicketListView { Id = 16, TicketNumber = "TKT-2025-016", Subject = "Cache Issue", Title = "Old data showing", Description = "Cache not invalidating properly", Status = "Open", Priority = "Medium", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-15), LastUpdate = DateTime.UtcNow.AddDays(-14) },
                new TicketListView { Id = 17, TicketNumber = "TKT-2025-017", Subject = "UI Enhancement", Title = "Add dark mode", Description = "Users requested dark mode feature", Status = "Resolved", Priority = "Low", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-14), LastUpdate = DateTime.UtcNow.AddDays(-13) },
                new TicketListView { Id = 18, TicketNumber = "TKT-2025-018", Subject = "Memory Leak", Title = "Server memory usage high", Description = "Memory leak detected on API service", Status = "In Progress", Priority = "High", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-13), LastUpdate = DateTime.UtcNow.AddDays(-12) },
                new TicketListView { Id = 19, TicketNumber = "TKT-2025-019", Subject = "UI Freeze", Title = "App freezes on scroll", Description = "UI freezes when scrolling large table", Status = "Closed", Priority = "High", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-12), LastUpdate = DateTime.UtcNow.AddDays(-11) },
                new TicketListView { Id = 20, TicketNumber = "TKT-2025-020", Subject = "File Upload", Title = "Large file not uploading", Description = "Upload fails for >50MB files", Status = "Open", Priority = "Medium", Assignee = "Sohan", CreatedDate = DateTime.UtcNow.AddDays(-11), LastUpdate = DateTime.UtcNow.AddDays(-10) },
                new TicketListView { Id = 21, TicketNumber = "TKT-2025-021", Subject = "API Error", Title = "External API timeout", Description = "3rd party API not responding", Status = "In Progress", Priority = "High", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-10), LastUpdate = DateTime.UtcNow.AddDays(-9) },
                new TicketListView { Id = 22, TicketNumber = "TKT-2025-022", Subject = "Cross-Site Scripting", Title = "XSS vulnerability", Description = "Security team found XSS issue", Status = "Open", Priority = "High", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-9), LastUpdate = DateTime.UtcNow.AddDays(-8) },
                new TicketListView { Id = 23, TicketNumber = "TKT-2025-023", Subject = "Search Issue", Title = "Search not working", Description = "Search returns no results", Status = "Closed", Priority = "Medium", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-8), LastUpdate = DateTime.UtcNow.AddDays(-7) },
                new TicketListView { Id = 24, TicketNumber = "TKT-2025-024", Subject = "Login Bug", Title = "2FA not working", Description = "Two-factor authentication fails", Status = "Resolved", Priority = "Medium", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-7), LastUpdate = DateTime.UtcNow.AddDays(-6) },
                new TicketListView { Id = 25, TicketNumber = "TKT-2025-025", Subject = "Server Crash", Title = "App server down", Description = "Production server crashed", Status = "Open", Priority = "High", Assignee = "Sohan", CreatedDate = DateTime.UtcNow.AddDays(-6), LastUpdate = DateTime.UtcNow.AddDays(-5) },
                new TicketListView { Id = 26, TicketNumber = "TKT-2025-026", Subject = "Session Expired", Title = "Auto logout", Description = "Session expires after 2 mins", Status = "In Progress", Priority = "Medium", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-5), LastUpdate = DateTime.UtcNow.AddDays(-4) },
                new TicketListView { Id = 27, TicketNumber = "TKT-2025-027", Subject = "Report Error", Title = "Excel export failed", Description = "Exporting reports to Excel not working", Status = "Open", Priority = "Medium", Assignee = "Shamim", CreatedDate = DateTime.UtcNow.AddDays(-4), LastUpdate = DateTime.UtcNow.AddDays(-3) },
                new TicketListView { Id = 28, TicketNumber = "TKT-2025-028", Subject = "Order Issue", Title = "Order stuck in processing", Description = "Order not moving from processing to completed", Status = "Resolved", Priority = "High", Assignee = "Lorna", CreatedDate = DateTime.UtcNow.AddDays(-3), LastUpdate = DateTime.UtcNow.AddDays(-2) },
                new TicketListView { Id = 29, TicketNumber = "TKT-2025-029", Subject = "Integration Bug", Title = "Payment gateway error", Description = "Payments failing for VISA cards", Status = "In Progress", Priority = "High", Assignee = "Brian", CreatedDate = DateTime.UtcNow.AddDays(-2), LastUpdate = DateTime.UtcNow.AddDays(-1) },
                new TicketListView { Id = 30, TicketNumber = "TKT-2025-030", Subject = "Email Bounce", Title = "Emails bouncing back", Description = "Customer emails rejected by mail server", Status = "Closed", Priority = "Low", Assignee = "Saiful", CreatedDate = DateTime.UtcNow.AddDays(-1), LastUpdate = DateTime.UtcNow }
            };
            }
        }
    }

}
