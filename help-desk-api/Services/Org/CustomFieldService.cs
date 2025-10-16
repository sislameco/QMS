using Microsoft.EntityFrameworkCore;
using Models.Dto.Org;
using Models.Entities;
using Models.Entities.Org;
using Repository;

namespace Services.Org
{
    public interface ICustomFieldService
    {
        Task<IEnumerable<CustomFieldInputDto>> GetAllAsync();
        Task<CustomFieldInputDto?> GetByIdAsync(int id);
        Task<bool> CreateManyAsync(CustomFieldInputDto dtos);
        Task<CustomFieldInputDto?> UpdateAsync(int id, CustomFieldInputDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class CustomFieldService : ICustomFieldService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomFieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomFieldInputDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<CustomFieldModel, int>().GetAllAsync();
            return entities.ConvertAll(MapToDto);
        }

        public async Task<CustomFieldInputDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<CustomFieldModel, int>().GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<bool> CreateManyAsync(CustomFieldInputDto input)
        {
            var addField = new CustomFieldModel
            {
                FkTicketTypeId = 1,
                DisplayName = input.DisplayName,
                DataType = input.DataType,
                IsRequired = input.IsRequired,
                DDLValue = input.DDLValue,
                Description = input.Description,
                IsMultiSelect = input.IsMultiSelect
            };

            await _unitOfWork.Repository<CustomFieldModel, int>().AddAsync(addField);
            return await _unitOfWork.CommitAsync() > 0 ? true: false;

        }

        public async Task<CustomFieldInputDto?> UpdateAsync(int id, CustomFieldInputDto dto)
        {
            var entity = await _unitOfWork.Repository<CustomFieldModel, int>().GetByIdAsync(id);
            if (entity == null) return null;
            entity.FkTicketTypeId = dto.FkTicketTypeId;
            entity.DisplayName = dto.DisplayName;
            entity.DataType = dto.DataType;
            entity.IsRequired = dto.IsRequired;
            entity.DDLValue = dto.DDLValue;
            entity.Description = dto.Description;
            entity.IsMultiSelect = dto.IsMultiSelect;
            // Map other properties as needed
            _unitOfWork.Repository<CustomFieldModel, int>().Update(entity);
            await _unitOfWork.CommitAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<CustomFieldModel, int>().GetByIdAsync(id);
            if (entity == null) return false;
            await _unitOfWork.Repository<CustomFieldModel, int>().DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return true;
        }

        private static CustomFieldInputDto MapToDto(CustomFieldModel model)
        {
            return new CustomFieldInputDto
            {
                FkTicketTypeId = model.FkTicketTypeId,
                DisplayName = model.DisplayName,
                DataType = model.DataType,
                IsRequired = model.IsRequired,
                DDLValue = model.DDLValue,
                Description = model.Description,
                IsMultiSelect = model.IsMultiSelect
                // Add mapping for other properties if CustomFieldDto is extended
            };
        }

        private static CustomFieldModel MapToModel(CustomFieldInputDto dto)
        {
            return new CustomFieldModel
            {
                FkTicketTypeId = dto.FkTicketTypeId,
                DisplayName = dto.DisplayName,
                DataType = dto.DataType,
                IsRequired = dto.IsRequired,
                 DDLValue = dto.DDLValue,
                Description = dto.Description
                // Add mapping for other properties if CustomFieldModel is extended
            };
        }
    }
}
