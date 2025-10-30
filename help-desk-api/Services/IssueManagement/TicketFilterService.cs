

using Repository;

namespace Services.IssueManagement
{
    public interface ITicketFilterService
    {
    }

    public class TicketFilterService : ITicketFilterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketFilterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}