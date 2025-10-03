using Models.Dto.Org;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using Utils.LoginData;

namespace Services.Org
{
    public interface ICompanyService
    {
        Task<bool> UpdateCompanyAsync(int id, CompanyDto dto);
        Task<List<CompanyDto>> GetActiveCompaniesAsync();
        Task<CompanyDto> GetCompany(int id);
    }
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfos _userInfos;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CompanyDto>> GetActiveCompaniesAsync()
        {
            var companies = await _unitOfWork.Repository<CompanyModel, int>()
                .FindByConditionAsync(c => c.RStatus == EnumRStatus.Active);
            return companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                ShortName = c.ShortName,
                Description = c.Description,
                AccessKey = c.AccessKey,
                SecretKey = c.SecretKey,
                PrefixTicket = c.PrefixTicket,
                LastTicketNumber = c.LastTicketNumber,
                DefineDataSources = c.CompanyDefineData.Where(s => s.RStatus == EnumRStatus.Active).Count()> 0?  c.CompanyDefineData.Where(s=> s.RStatus == EnumRStatus.Active).Select(s=> new CompanyDefineDataSourceDto
                {
                    Id = s.Id,
                     IsSync = s.IsSync,
                    IsValidate = s.IsValidate,
                     JsonData = s.JsonData,
                     Source = s.Source 
                }).ToList(): new List<CompanyDefineDataSourceDto>()
            }).ToList();
        }
        public async Task<bool> UpdateCompanyAsync(int id, CompanyDto dto)
        {
            var company = await _unitOfWork.Repository<CompanyModel, int>().GetByIdAsync(id);
            if (company == null)
                throw new Exception("Company not found");

            company.Name = dto.Name;
            company.ShortName = dto.ShortName;
            company.Description = dto.Description;
            company.PrefixTicket = dto.PrefixTicket;
            company.LastTicketNumber = dto.LastTicketNumber;

            foreach(var defineData in dto.DefineDataSources)
            {
                if (defineData.Id == 0)
                {
                    CompanyDefineDataSourceModel companyDefineDataSource = new CompanyDefineDataSourceModel
                    {
                        FkCompanyId = dto.Id,
                        IsSync = defineData.IsSync,
                        IsValidate = defineData.IsValidate,
                        JsonData = defineData.JsonData,
                        Source = defineData.Source,
                        RStatus = EnumRStatus.Active,
                    };
                    company.CompanyDefineData.Add(companyDefineDataSource);
                }
                else {
                    CompanyDefineDataSourceModel upcompanyDefineDataSource = company.CompanyDefineData.FirstOrDefault(c => c.Id == defineData.Id) ?? null;
                    if (upcompanyDefineDataSource != null)
                    {
                        upcompanyDefineDataSource.IsSync = defineData.IsSync;
                        upcompanyDefineDataSource.IsValidate = defineData.IsValidate;
                        upcompanyDefineDataSource.JsonData = defineData.JsonData;
                        upcompanyDefineDataSource.Source = defineData.Source;
                    }
                    company.CompanyDefineData.Add(upcompanyDefineDataSource);
                }
            }
            await _unitOfWork.Repository<CompanyModel, int>().UpdateAsync(company);
            return await _unitOfWork.CommitAsync() > 0;

        }
        public async Task<CompanyDto> GetCompany(int id)
        {
            var repo = _unitOfWork.Repository<CompanyModel, int>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return null;

            return new CompanyDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ShortName = entity.ShortName,
                Description = entity.Description,
                AccessKey = entity.AccessKey,
                SecretKey = entity.SecretKey,
                PrefixTicket = entity.PrefixTicket,
                LastTicketNumber = entity.LastTicketNumber,
                DefineDataSources = entity.CompanyDefineData.Where(s => s.RStatus == EnumRStatus.Active).Select(s => new CompanyDefineDataSourceDto
                {
                    Id = s.Id,
                    IsSync = s.IsSync,
                    IsValidate = s.IsValidate,
                    JsonData = s.JsonData,
                    Source = s.Source
                }).ToList()

            };
        }
    }
}
