using Models.Dto.Org;
using Models.Entities.Org;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Utils.Intregation;
using Utils.LoginData;

namespace Services.Org
{
    public interface ICompanyService
    {
        Task<bool> UpdateCompanyAsync(int id, CompanyDto dto);
        Task<List<CompanyDto>> GetActiveCompaniesAsync();
        Task<CompanyDto> GetCompany(int id);
        Task<bool> Sync(int id, int companyId, EnumDataSource type);
    }
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHRService _hRService;
        public CompanyService(IUnitOfWork unitOfWork, IHRService hRService)
        {
            _unitOfWork = unitOfWork;
            _hRService = hRService;
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
                DefineDataSources = c.CompanyDefineData.Where(s => s.RStatus == EnumRStatus.Active).Count() > 0 ? c.CompanyDefineData.Where(s => s.RStatus == EnumRStatus.Active).Select(s => new CompanyDefineDataSourceDto
                {
                    Id = s.Id,
                    IsSync = s.IsSync,
                    JsonData = s.JsonData,
                    Source = s.Source
                }).ToList() : new List<CompanyDefineDataSourceDto>()
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

            foreach (var defineData in dto.DefineDataSources)
            {
                if (defineData.Id == 0)
                {
                    CompanyDefineDataSourceModel companyDefineDataSource = new CompanyDefineDataSourceModel
                    {
                        FkCompanyId = dto.Id,
                        IsSync = defineData.IsSync,
                        JsonData = defineData.JsonData,
                        Source = defineData.Source,
                        RStatus = EnumRStatus.Active,
                    };
                    company.CompanyDefineData.Add(companyDefineDataSource);
                }
                else
                {
                    CompanyDefineDataSourceModel upcompanyDefineDataSource = company.CompanyDefineData.FirstOrDefault(c => c.Id == defineData.Id) ?? null;
                    if (upcompanyDefineDataSource != null)
                    {
                        upcompanyDefineDataSource.IsSync = defineData.IsSync;
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
                    JsonData = s.JsonData,
                    Source = s.Source
                }).ToList()

            };
        }

        public async Task<bool> Sync(int id, int companyId, EnumDataSource type)
        {
            bool isSync = false;
            switch (type)
            {
                case EnumDataSource.User:
                    isSync = await SyncDepartments(id, companyId);

                    break;
                case EnumDataSource.Department:
                    isSync = await SyncUsers(id, companyId);

                    break;
                default:
                    throw new Exception("Invalid data source type");
            }
            return isSync;
        }

        private async Task<bool> SyncUsers(int id, int CompanyId)
        {
            try
            {
                var users = await _hRService.GetUsersAsync();
                if (users.Any())
                {
                    var companyData = await _unitOfWork.Repository<CompanyDefineDataSourceModel, int>().FirstOrDefaultAsync(s => s.Id == id);
                    if (companyData == null)
                        throw new Exception("Company Define Data Source not found");
                    companyData.JsonData = System.Text.Json.JsonSerializer.Serialize(users);
                    companyData.IsSync = true;
                    _unitOfWork.Repository<CompanyDefineDataSourceModel, int>().Update(companyData);

                    foreach (var user in users)
                    {
                        UserModel addUser = new UserModel
                        {
                            FkCompanyId = CompanyId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.EmailAddress,
                            UserName = user.UserName,
                            FullName = $"{user.FirstName} {user.LastName}",
                            Phone = string.Empty,
                            PasswordHash = string.Empty,
                            IntegrationsPrimaryId = user.UserId,
                            RStatus = EnumRStatus.Active,
                        };
                        await _unitOfWork.Repository<UserModel, int>().AddAsync(addUser);
                    }
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task<bool> SyncDepartments(int id, int CompanyId)
        {
            try
            {
                var departments = await _hRService.GetDepartmentsAsync();
                if (departments.Any())
                {
                    var companyData = await _unitOfWork.Repository<CompanyDefineDataSourceModel, int>().FirstOrDefaultAsync(s => s.Id == id);
                    if (companyData == null)
                        throw new Exception("Company Define Data Source not found");

                    companyData.JsonData = System.Text.Json.JsonSerializer.Serialize(departments);
                    companyData.IsSync = true;
                    _unitOfWork.Repository<CompanyDefineDataSourceModel, int>().Update(companyData);

                    foreach (var department in departments)
                    {
                        DepartmentModel addDep = new DepartmentModel
                        {
                            Name = department.Name,
                            Description = string.Empty,
                            FKCompanyId = CompanyId,
                            FKManagerId = null,
                            IntegrationsPrimaryId = department.Id
                        };
                        await _unitOfWork.Repository<DepartmentModel, int>().AddAsync(addDep);
                    }
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
