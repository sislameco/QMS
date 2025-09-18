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
        public Task<TicketOutPutDto> GetTicketSummary();
    }
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HelpDbContext _context;

        public DashboardRepository(HelpDbContext context)
        {
            _context = context;
        }
        public async Task<TicketOutPutDto> GetTicketSummary()
        {
            return new TicketOutPutDto()
            {
                Opens = 10,
                Recent = 2,
                Views = 5
            };
        }
    }
}
