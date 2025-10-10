using Models.Dto.Org;
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
        Task<List<SLAOutputDto>> GetAllAsync();
        Task<SLAInputDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SLAInputDto dto);
        Task<bool> UpdateAsync(int id, SLAInputDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class SLAService : ISLAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SLAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SLAOutputDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<SLAConfigurationModel, int>().GetAllAsync();
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
            if (!await IsTypePriorityUniqueAsync(dto.Type, dto.Priority, dto.FKCompanyId))
                throw new BadRequestException("QMS Type and Priority Level are must be unique!");

            var entity = MapToEntity(dto);
            await _unitOfWork.Repository<SLAConfigurationModel, int>().AddAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, SLAInputDto dto)
        {
            await IsTypePriorityUniqueAsync(dto.Type, dto.Priority, dto.FKCompanyId, id);
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new BadRequestException("No SLA Found");

            // Update fields
            entity.Type = dto.Type;
            entity.Priority = dto.Priority;
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

            await repo.DeleteAsync(id);
            return await _unitOfWork.CommitAsync() > 0;
        }

        // Mapping helpers
        private static SLAOutputDto MapToDto(SLAConfigurationModel entity) => new SLAOutputDto
        {
            Id = entity.Id,
            Type = entity.Type,
            Priority = entity.Priority,
            FKCompanyId = entity.FKCompanyId,
            Unit = entity.Unit,
            ResponseTime = entity.ResponseTime,
            ResolutionTime = entity.ResolutionTime,
            EscalationTime = entity.ResolutionTime,
            Status = entity.RStatus
        };

        private static SLAConfigurationModel MapToEntity(SLAInputDto dto) => new SLAConfigurationModel
        {
            Type = dto.Type,
            Priority = dto.Priority,
            FKCompanyId = dto.FKCompanyId,
            Unit = dto.Unit,
            ResponseTime = dto.ResponseTime,
            ResolutionTime = dto.ResolutionTime,
            EscalationTime = dto.ResolutionTime,
            RStatus = EnumRStatus.Active
        };
        private async Task<bool> IsTypePriorityUniqueAsync(EnumQMSType type, EnumPriority priority, int companyId, int id = 0)
        {
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var exists = await repo.ExistsAsync(x =>
                x.Type == type &&
                x.Priority == priority &&
                x.FKCompanyId == companyId &&
                x.Id != id &&
                x.RStatus == EnumRStatus.Active);
            return !exists;
        }
    }
}
