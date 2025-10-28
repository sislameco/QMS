using Models.Dto.Org;
using Models.Entities.Notification;
using Models.Enum;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Org
{
    public interface INotificationTemplateService
    {
        Task<bool> UpdateTemplateAsync(NotificationInputDto input);
        Task<bool> UpdateIsEnabledAsync(int id, bool isEnabled);
        Task<List<NotificationOutputDto>> GetAllActiveByCompanyIdAsync(int fkCompanyId, EnumNotificationType type);
    }
    public class NotificationTemplateService : INotificationTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateTemplateAsync(NotificationInputDto input)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entity = await _unitOfWork.Repository<NotificationTemplateModel, int>().GetByIdAsync(input.Id);
            if (entity == null)
                throw new System.Exception("NotificationTemplate not found");
            entity.SubjectTemplate = input.SubjectTemplate;
            entity.BodyTemplate = input.BodyTemplate;

            _unitOfWork.Repository<NotificationTemplateModel, int>().Update(entity);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> UpdateIsEnabledAsync(int id, bool isEnabled)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new System.Exception("NotificationTemplate not found");

            entity.IsEnabled = isEnabled;
            repo.Update(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<NotificationOutputDto>> GetAllActiveByCompanyIdAsync(int fkCompanyId, EnumNotificationType type)
        {
            var repo = _unitOfWork.Repository<NotificationTemplateModel, int>();
            var entities = repo.FindByConditionSelected(
                x => x.FkCompanyId == fkCompanyId && x.RStatus == EnumRStatus.Active && x.NotificationType == type,
                x => new { x.Id, x.BodyTemplate, x.SubjectTemplate, x.Variables, x.IsEnabled, x.Event, x.EmailConfigurationId, x.NotificationType });

            return entities.Select(x => new NotificationOutputDto
            {
                Id = x.Id,
                Event = x.Event,
                NotificationType = x.NotificationType,
                BodyTemplate = x.BodyTemplate,
                SubjectTemplate = x.SubjectTemplate,
                IsEnabled = x.IsEnabled,
                Variables = x.Variables
            }).ToList();
        }
    }
}