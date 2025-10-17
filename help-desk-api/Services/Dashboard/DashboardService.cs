using Models.Dto.Dashboard;
using Repository.Repo.UserManagement;

namespace Services.Dashboard
{
    public interface IDashboardService
    {
        public Task<DashboardResponseDto> GetDashboard();
    }
    public class DashboardService : IDashboardService
    {
        IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<DashboardResponseDto> GetDashboard()
        {
            return  await _dashboardRepository.GetDashboard();
        }
    }
}
