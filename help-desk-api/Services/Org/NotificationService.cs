using Models.Entities.Notification;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Org
{
    public interface INotificationService
    {
        Task<bool> UpdateTemplateAsync(int id, int emailConfigurationId, string subjectTemplate, string bodyTemplate, string ccList);
        Task<bool> UpdateIsEnabledAsync(int id, bool isEnabled);
        Task<List<NotificationTemplateModel>> GetAllActiveByCompanyIdAsync(int fkCompanyId);
    }
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateTemplateAsync(int id, int emailConfigurationId, string subjectTemplate, string bodyTemplate, string ccList)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new System.Exception("NotificationTemplate not found");

            entity.EmailConfigurationId = emailConfigurationId;
            entity.SubjectTemplate = subjectTemplate;
            entity.BodyTemplate = bodyTemplate;

            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateIsEnabledAsync(int id, bool isEnabled)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new System.Exception("NotificationTemplate not found");

            entity.IsEnabled = isEnabled;
            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<NotificationTemplateModel>> GetAllActiveByCompanyIdAsync(int fkCompanyId)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entities = await repo.FindByConditionAsync(x => x.FkCompanyId == fkCompanyId && x.RStatus == EnumRStatus.Active);
            return entities.ToList();
        }
    }
}