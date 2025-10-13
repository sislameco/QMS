using Models.Dto.CustomDefine;
using Models.Entities.Setup;
using Models.Enum;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Services.Setup
{
    public interface ICompanyDefineRootResolutionService
    {
        Task<List<RootCauseOutDto>> GetAllAsync(int companyId, EnumRootResolutionType type);
        Task<RootCauseOutDto> GetByIdAsync(int id);
        Task<bool> SaveAsync(RootCauseInputDto model);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangeDisplayOrder(int id, int order);
    }

    public class CompanyDefineRootResolutionService : ICompanyDefineRootResolutionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyDefineRootResolutionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RootCauseOutDto>> GetAllAsync(int companyId, EnumRootResolutionType type)
        {
            var repo = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>();
            var query = await repo.GetAllAsync();
            return query
                .Where(x => x.FKCompanyId == companyId
                    && x.Type == type
                    && (x.RStatus == EnumRStatus.Active))
                .Select(x => new RootCauseOutDto
                {
                    Description = x.Description,
                    DisplayOrder = x.DisplayOrder,
                    FKCompanyId = x.FKCompanyId,
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                })
                .ToList();
        }

        public async Task<RootCauseOutDto> GetByIdAsync(int id)
        {
            var data = await _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>().GetByIdAsync(id);
            if (data == null) throw new BadRequestException("Invalid Request!");
            return new RootCauseOutDto
            {
                Description = data.Description,
                DisplayOrder = data.DisplayOrder,
                FKCompanyId = data.FKCompanyId,
                Id = data.Id,
                Name = data.Name,
                Type = data.Type
            };
        }

        public async Task<bool> SaveAsync(RootCauseInputDto input)
        {

            var repo = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>();

            var values = await repo.FindByConditionAsync(s => s.Id != input.Id && s.Name == input.Name && s.Type == input.Type);
            if (values.Any()) throw new BadRequestException("Name must be unique!");
                
            if (input.Id == 0 && input.Task == EnumCrud.Create)
            {


                CompanyDefineRootResolutionModel add = new CompanyDefineRootResolutionModel
                {
                    Name = input.Name,
                    Description = input.Description,
                    DisplayOrder = input.DisplayOrder,
                    Type = input.Type,
                    FKCompanyId = input.FKCompanyId
                };
                await repo.AddAsync(add);
            }
            else if (input.Id > 0 && input.Task == EnumCrud.Create)
            {
                var entity = await repo.GetByIdAsync(input.Id);
                if (entity == null) return false;

                entity.Name = input.Name;
                entity.Description = input.Description;
                entity.DisplayOrder = input.DisplayOrder;
                entity.Type = input.Type;
                entity.FKCompanyId = input.FKCompanyId;
                entity.RStatus = EnumRStatus.Active;
                await repo.UpdateAsync(entity);
            }
            else
                throw new BadRequestException("Invalid Request!");

            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;
            await repo.DeleteAsync(id);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> ChangeDisplayOrder(int id, int order)
        {
            var repo = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;
            entity.DisplayOrder = order;

            await repo.UpdateAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}