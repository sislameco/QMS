using Models.Entities.Audit;
using Repository;

namespace Services.Global
{
    public interface IAuditLogService
    {
        Task AddAsync(QMSAuditLogModel log);
    }
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(QMSAuditLogModel log)
        {
            await _unitOfWork.WithOutRepository<QMSAuditLogModel,int>().AddAsync(log);
            await _unitOfWork.CommitAsync();
        }
    }
}
