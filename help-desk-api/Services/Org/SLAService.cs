using Models.Dto.Org;
using Repository;
using Models.Entities.Org;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Enum;
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
            var entity = MapToEntity(dto);
            await _unitOfWork.Repository<SLAConfigurationModel, int>().AddAsync(entity);
            return await _unitOfWork.CommitAsync()> 0;
        }

        public async Task<bool> UpdateAsync(int id, SLAInputDto dto)
        {
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
            return await _unitOfWork.CommitAsync()> 0;
        }

        // Mapping helpers
        private static SLAOutputDto MapToDto(SLAConfigurationModel entity) => new SLAOutputDto
        {
            Id = entity.Id,
            Type = entity.Type,
            Priority = entity.Priority,
            FKCompanyId = 1,
            Unit = entity.Unit,
            ResponseTime = entity.ResponseTime,
            ResolutionTime = entity.ResolutionTime,
            EscalationTime = entity.ResolutionTime,
        };

        private static SLAConfigurationModel MapToEntity(SLAInputDto dto) => new SLAConfigurationModel
        {
            Type = dto.Type,
            Priority = dto.Priority,
            FKCompanyId = 1,
            Unit = dto.Unit,
            ResponseTime = dto.ResponseTime,
            ResolutionTime = dto.ResolutionTime,
            EscalationTime = dto.ResolutionTime,
            RStatus = EnumRStatus.Active
        };
    }
}
