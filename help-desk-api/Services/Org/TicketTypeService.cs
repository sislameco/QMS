using Models.Dto.Org;
using Models.Entities.Issue;
using Models.Entities.Org;
using Repository;

namespace Services.Org
{
    public interface ITicketTypeService
    {
        Task<List<TicketTypeOutputDto>> GetAllAsync();
        Task<TicketTypeOutputDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(TicketTypeInputDto dto);
        Task<bool> UpdateAsync(int id, TicketTypeInputDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class TicketTypeService : ITicketTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TicketTypeOutputDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<TicketTypeModel, int>().GetAllAsync();
            return (List<TicketTypeOutputDto>)entities.Select(MapToDto);
        }

        public async Task<TicketTypeOutputDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<TicketTypeModel, int>().GetByIdAsync(id);
            return entity == null ? null : await MapToDto(entity);
        }

        public async Task<bool> CreateAsync(TicketTypeInputDto dto)
        {
            var entity = MapToEntity(dto);
            await _unitOfWork.Repository<TicketTypeModel, int>().AddAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> UpdateAsync(int id, TicketTypeInputDto dto)
        {
            var repo = _unitOfWork.Repository<TicketTypeModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;

            // Update fields
            entity.Priority = dto.Priority;
            entity.FKAssignedUserId = dto.FKAssignedUserId;
            entity.IsEnabled = dto.IsEnabled;
            entity.FKDepartmentIds = dto.FKDepartmentIds;
            entity.Description = dto.Description;
            entity.Title = dto.Title;
            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<TicketTypeModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;

            await repo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return true;
        }

        // Mapping helpers
        private   async Task<TicketTypeOutputDto> MapToDto(TicketTypeModel entity) => new TicketTypeOutputDto
        {
            FKAssignedUserId = entity.FKAssignedUserId,
            Priority = entity.Priority,
            IsEnabled = entity.IsEnabled,
            FKDepartmentIds = entity.FKDepartmentIds,
            FKCompanyId = entity.FKCompanyId,
            Id = entity.Id,
            Description = entity.Description,
            DepartmentNames = _unitOfWork.Repository<DepartmentModel, int>()
                                .FindByConditionOneColumn(d => entity.FKDepartmentIds.Contains(d.Id),s=> s.Name).ToArray(),
        };

        private static TicketTypeModel MapToEntity(TicketTypeInputDto dto) => new TicketTypeModel
        {
            Title = dto.Title,
            Description = dto.Description,
            IsEnabled = dto.IsEnabled,
            Priority = dto.Priority,
            FKAssignedUserId = dto.FKAssignedUserId,
            FKDepartmentIds = dto.FKDepartmentIds,
            FKCompanyId = dto.FKCompanyId
        };
    }
}
