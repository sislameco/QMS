using Models.Entities.Org;
using Models.Entities.Setup;
using Models.Enum;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Org
{
    public interface IEmailConfigurationService
    {
        Task<bool> UpdateFieldsAsync(EmailConfigInputDto input);
        Task<bool> SetDefaultAsync(int id);
        Task<List<EmailConfigurationModel>> GetAllActiveByCompanyIdAsync(int fkCompanyId);
    }
    public class EmailConfigurationService : IEmailConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateFieldsAsync(EmailConfigInputDto input)
        {
            var repo = _unitOfWork.Repository<EmailConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(input.Id);
            if (entity == null)
                throw new System.Exception("EmailConfiguration not found");

            entity.UserName = input.UserName;
            entity.Name = input.Name;
            entity.ReplyTo = input.ReplyTo;
            entity.BCC = input.Bcc;
            entity.CcList = input.CcList;

            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> SetDefaultAsync(int id)
        {
            var repo = _unitOfWork.Repository<EmailConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new System.Exception("EmailConfiguration not found");

            // Set all others to false
            var allConfigs = await repo.FindByConditionAsync(x => x.RStatus == EnumRStatus.Active);
            foreach (var config in allConfigs)
            {
                config.IsDefault = config.Id == id;
                await repo.UpdateAsync(config);
            }
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<EmailConfigurationModel>> GetAllActiveByCompanyIdAsync(int fkCompanyId)
        {
            var repo = _unitOfWork.Repository<EmailConfigurationModel, int>();
            var entities = await repo.FindByConditionAsync(x => x.RStatus == EnumRStatus.Active);
            return entities.ToList();
        }
    }
}