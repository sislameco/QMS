using Models.Dto.Org;
using Repository;
using Models.Entities.Org;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Org
{
    public interface ISLAService
    {
        Task<IEnumerable<SLAInputDto>> GetAllAsync();
        Task<SLAInputDto?> GetByIdAsync(int id);
        Task<SLAInputDto> CreateAsync(SLAInputDto dto);
        Task<SLAInputDto?> UpdateAsync(int id, SLAInputDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class SLAService : ISLAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SLAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SLAInputDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<SLAConfigurationModel, int>().GetAllAsync();
            return entities.Select(MapToDto);
        }

        public async Task<SLAInputDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<SLAConfigurationModel, int>().GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<SLAInputDto> CreateAsync(SLAInputDto dto)
        {
            var entity = MapToEntity(dto);
            await _unitOfWork.Repository<SLAConfigurationModel, int>().AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return MapToDto(entity);
        }

        public async Task<SLAInputDto?> UpdateAsync(int id, SLAInputDto dto)
        {
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return null;

            // Update fields
            entity.Type = dto.Type;
            entity.Priority = dto.Priority;
            entity.FKCompanyId = dto.FKCompanyId;
            entity.Unit = dto.Unit;
            entity.Value = dto.Value;

            await repo.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<SLAConfigurationModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;

            await repo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return true;
        }

        // Mapping helpers
        private static SLAInputDto MapToDto(SLAConfigurationModel entity) => new SLAInputDto
        {
            Type = entity.Type,
            Priority = entity.Priority,
            FKCompanyId = entity.FKCompanyId,
            Unit = entity.Unit,
            Value = entity.Value
        };

        private static SLAConfigurationModel MapToEntity(SLAInputDto dto) => new SLAConfigurationModel
        {
            Type = dto.Type,
            Priority = dto.Priority,
            FKCompanyId = dto.FKCompanyId,
            Unit = dto.Unit,
            Value = dto.Value
        };
    }
}
