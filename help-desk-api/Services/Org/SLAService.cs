using Models.Dto.Org;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Services.Org
{
    public interface ISLAService
    {
        Task<List<SLAOutputDto>> GetAllAsync(int fkCompanyId);
        Task<SLAInputDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SLAInputDto dto);
        Task<bool> UpdateAsync(int id, SLAInputDto dto);
        Task<bool> DeleteAsync(int id);
        SLATileViewOutputDto GetTile(int companyId);
    }

    public class SLAService : ISLAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SLAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SLAOutputDto>> GetAllAsync(int fkCompanyId)
        {
            var entities = await _unitOfWork.Repository<SLAConfigurationModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active && s.FKCompanyId == fkCompanyId, x => x.TicketType);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SLAInputDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<SLAConfigurationModel, int>().GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<bool> CreateAsync(SLAInputDto dto)
        {
            // validation EnumQMSType and EnumPriority should be unique 
            if (!await IsTypePriorityUniqueAsync(dto.FKTicketTypeId, dto.FKCompanyId))
                throw new BadRequestException("QMS Type and Priority Level are must be unique!");

            var entity = MapToEntity(dto);
            await _unitOfWork.Repository<SLAConfigurationModel, int>().AddAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, SLAInputDto dto)
        {
            await IsTypePriorityUniqueAsync(dto.FKTicketTypeId, dto.FKCompanyId, id);
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new BadRequestException("No SLA Found");

            // Update fields
            entity.FKTicketTypeId = dto.FKTicketTypeId;
            entity.FKCompanyId = dto.FKCompanyId;
            entity.Unit = dto.Unit;
            entity.ResponseTime = dto.ResponseTime;
            entity.ResolutionTime = dto.ResolutionTime;
            entity.EscalationTime = dto.ResolutionTime;

            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;

            await _unitOfWork.Repository<SLAConfigurationModel, int>().DeleteAsync(id);
            return await _unitOfWork.CommitAsync() > 0;
        }

        // Mapping helpers
        private static SLAOutputDto MapToDto(SLAConfigurationModel entity) => new SLAOutputDto
        {
            Id = entity.Id,
            FKTicketTypeId = entity.FKTicketTypeId,
            FKCompanyId = entity.FKCompanyId,
            Unit = entity.Unit,
            ResponseTime = entity.ResponseTime,
            ResolutionTime = entity.ResolutionTime,
            EscalationTime = entity.ResolutionTime,
            Status = entity.RStatus,
            Priority = entity.TicketType != null ? entity.TicketType.Priority : default,
            TypeTitle = entity.TicketType != null ? entity.TicketType.Title.ToString() : default,
            QmsType = entity.TicketType != null ? entity.TicketType.QmsType : default,
        };

        private static SLAConfigurationModel MapToEntity(SLAInputDto dto) => new SLAConfigurationModel
        {
            FKTicketTypeId = dto.FKTicketTypeId,
            FKCompanyId = dto.FKCompanyId,
            Unit = dto.Unit,
            ResponseTime = dto.ResponseTime,
            ResolutionTime = dto.ResolutionTime,
            EscalationTime = dto.ResolutionTime,
            RStatus = EnumRStatus.Active
        };
        private async Task<bool> IsTypePriorityUniqueAsync(int fkTicketTypeId, int companyId, int id = 0)
        {
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var exists = await repo.ExistsAsync(x =>
                x.FKTicketTypeId == fkTicketTypeId &&
                x.FKCompanyId == companyId &&
                x.Id != id &&
                x.RStatus == EnumRStatus.Active);
            return !exists;
        }

        public SLATileViewOutputDto GetTile(int companyId)
        {
            return new()
            {
                ActiveRules = 10,
                AvgResponse = 10,
                CriticalRules = 10,
                TotalRules = 10
            };
        }
    }
}
