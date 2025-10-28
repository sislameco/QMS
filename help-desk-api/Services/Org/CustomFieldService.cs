using Models.Dto.GlobalDto;
using Models.Dto.Org;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using System.Linq;

namespace Services.Org
{
    public interface ICustomFieldService
    {
        Task<IEnumerable<CustomFieldInputDto>> GetAllAsync();
        Task<List<DropdownOutputDto<int, string>>> GetTicketTypesByFiled(int companyId);
        Task<CustomFieldInputDto?> GetByIdAsync(int id);
        Task<bool> CreateManyAsync(CustomFieldInputDto dtos);
        Task<CustomFieldInputDto?> UpdateAsync(int id, CustomFieldInputDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DisplayOrder(FieldDisplayOrderInputDto type);
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
        public async Task<List<DropdownOutputDto<int, string>>> GetTicketTypesByFiled(int companyId)
        {
            List<int> typeIds = _unitOfWork.Repository<CustomFieldModel, int>().FindByConditionSelected(s => s.RStatus == EnumRStatus.Active, s => s.FkTicketTypeId).ToList();
            var ticketTypes = _unitOfWork.Repository<TicketTypeModel, int>().FindByConditionSelected(s => s.RStatus == EnumRStatus.Active && s.FKCompanyId == companyId && typeIds.Contains(s.Id), s => new DropdownOutputDto<int, string> { Id = s.Id, Name = s.Title }).ToList();
            return ticketTypes;
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
                FkTicketTypeId = input.FkTicketTypeId,
                DisplayName = input.DisplayName,
                DataType = input.DataType,
                IsRequired = input.IsRequired,
                DDLValue = input.DDLValue,
                Description = input.Description,
                IsMultiSelect = input.IsMultiSelect
            };

            await _unitOfWork.Repository<CustomFieldModel, int>().AddAsync(addField);
            return await _unitOfWork.CommitAsync() > 0 ? true : false;

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
                Id = model.Id,
                FkTicketTypeId = model.FkTicketTypeId,
                TypeName = model.TicketType != null ? model.TicketType.Title : string.Empty,
                DisplayName = model.DisplayName,
                DataType = model.DataType,
                IsRequired = model.IsRequired,
                DDLValue = model.DDLValue,
                Description = model.Description,
                IsMultiSelect = model.IsMultiSelect,
                DisplayOrder = model.DisplayOrder
                // Add mapping for other properties if CustomFieldDto is extended
            };
        }
        public async Task<bool> DisplayOrder(FieldDisplayOrderInputDto type)
        {
            var entities = await _unitOfWork.Repository<CustomFieldModel, int>().GetAllAsync();
            foreach (var fieldId in type.FieldIds)
            {
                var entity = entities.FirstOrDefault(e => e.Id == fieldId && e.FkTicketTypeId == type.FkTicketTypeId);
                if (entity == null)
                {
                    throw new Exception($"Field with ID {fieldId} not found for Ticket Type ID {type.FkTicketTypeId}");
                }

                entity.DisplayOrder = type.FieldIds.IndexOf(fieldId) + 1; // +1 to start order from 1 instead of 0
                _unitOfWork.Repository<CustomFieldModel, int>().Update(entity);
            }
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}