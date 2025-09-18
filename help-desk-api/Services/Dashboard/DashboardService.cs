using Models.Dto.Dashboard;
using Repository;
using Repository.Repo.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.LoginData;

namespace Services.Dashboard
{
    public interface IDashboardService
    {
        public Task<TicketOutPutDto> GetTicketSummary();
    }
    public class DashboardService : IDashboardService
    {
        IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<TicketOutPutDto> GetTicketSummary()
        {
            return await _dashboardRepository.GetTicketSummary();
        }
    }
}
