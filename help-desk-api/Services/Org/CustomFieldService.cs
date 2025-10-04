using Microsoft.EntityFrameworkCore;
using Models.Dto.Org;
using Models.Entities;
using Models.Entities.Org;
using Newtonsoft.Json;
using Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Services.Org
{
    public interface ICustomFieldService
    {
        Task<IEnumerable<CustomFieldDto>> GetAllAsync();
        Task<CustomFieldDto?> GetByIdAsync(int id);
        Task<IEnumerable<CustomFieldDto>> CreateManyAsync(List<CustomFieldDto> dtos);
        Task<CustomFieldDto?> UpdateAsync(int id, CustomFieldDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class CustomFieldService : ICustomFieldService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomFieldService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomFieldDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<CustomFieldModel, int>().GetAllAsync();
            return entities.ConvertAll(MapToDto);
        }

        public async Task<CustomFieldDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<CustomFieldModel, int>().GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<IEnumerable<CustomFieldDto>> CreateManyAsync(List<CustomFieldDto> dtos)
        {
            var models = dtos.ConvertAll(MapToModel);
            foreach (var model in models)
            {
                await _unitOfWork.Repository<CustomFieldModel, int>().AddAsync(model);
            }
            await _unitOfWork.CommitAsync();
            return models.ConvertAll(MapToDto);
        }

        public async Task<CustomFieldDto?> UpdateAsync(int id, CustomFieldDto dto)
        {
            var entity = await _unitOfWork.Repository<CustomFieldModel, int>().GetByIdAsync(id);
            if (entity == null) return null;
            entity.FkTicketTypeId = dto.FkTicketTypeId;
            entity.DisplayName = dto.DisplayName;
            entity.DataType = dto.DataType;
            entity.IsRequired = dto.IsRequired;
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

        private static CustomFieldDto MapToDto(CustomFieldModel model)
        {
            return new CustomFieldDto
            {
                FkTicketTypeId = model.FkTicketTypeId,
                DisplayName = model.DisplayName,
                DataType = model.DataType,
                IsRequired = model.IsRequired
                // Add mapping for other properties if CustomFieldDto is extended
            };
        }

        private static CustomFieldModel MapToModel(CustomFieldDto dto)
        {
            return new CustomFieldModel
            {
                FkTicketTypeId = dto.FkTicketTypeId,
                DisplayName = dto.DisplayName,
                DataType = dto.DataType,
                IsRequired = dto.IsRequired
                // Add mapping for other properties if CustomFieldModel is extended
            };
        }
    }
}
