using Amazon.SimpleEmailV2.Model;
using Models.Dto.Intregation;
using Models.Dto.Menus;
using Models.Dto.Org;
using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Models.Entities.Org;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.Pagination;
using Repository.Repo.Permission;
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


        #region Departments
        Task<PaginationResponse<DepartmentSettingOutputDto>> GetAllDepartmentsAsync(int companyId, DepartmentSettingInputDto input);
        Task<bool> UpdateDepartmentAsync(DepartmentUpdateDto dto);
        public Task<DepartmentSetupOutputDto> GetDepartmentById(int id);
        #endregion
    }
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHRService _hRService;
        private readonly ICommonRepository _commonRepository;
        private readonly IUserInfos _userInfos;
        private readonly IMenuRepository _menuRepository;
        public CompanyService(IUnitOfWork unitOfWork, IHRService hRService, ICommonRepository commonRepository, IUserInfos userInfos, IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _hRService = hRService;
            _commonRepository = commonRepository;
            _userInfos = userInfos;
            _menuRepository = menuRepository;
        }
        public async Task<List<CompanyDto>> GetActiveCompaniesAsync()
        {
            var companies = await _unitOfWork.Repository<CompanyModel, int>()
                .FindByConditionAsync(c => c.RStatus == EnumRStatus.Active);
            var companyDefinSources = await _unitOfWork.Repository<CompanyDefineDataSourceModel, int>()
                .FindByConditionAsync(c => c.RStatus == EnumRStatus.Active && companies.Select(s => s.Id).Contains(c.FkCompanyId));
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
                DefineDataSources = companyDefinSources.Where(s => s.RStatus == EnumRStatus.Active && s.FkCompanyId == c.Id).Count() > 0 ? c.CompanyDefineData.Where(s => s.RStatus == EnumRStatus.Active).Select(s => new CompanyDefineDataSourceDto
                {
                    Id = s.Id,
                    IsSync = s.IsSync,
                    JSonData = s.JsonData,
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
                        JsonData = defineData.JSonData,
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
                        upcompanyDefineDataSource.JsonData = defineData.JSonData;
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
            var companyDefinSources = await _unitOfWork.Repository<CompanyDefineDataSourceModel, int>()
    .FindByConditionAsync(c => c.RStatus == EnumRStatus.Active && c.FkCompanyId == entity.Id);
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
                DefineDataSources = companyDefinSources.Where(s => s.RStatus == EnumRStatus.Active).Select(s => new CompanyDefineDataSourceDto
                {
                    Id = s.Id,
                    IsSync = s.IsSync,
                    Type = s.DataSourceType,
                    JSonData = s.JsonData,
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
                    isSync = await SyncUsers(id, companyId);

                    break;
                case EnumDataSource.Department:
                    isSync = await SyncDepartments(id, companyId);

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
        #region Departments

        public async Task<DepartmentSetupOutputDto> GetDepartmentById(int id)
        {
            var department = await _unitOfWork.Repository<DepartmentModel, int>().GetByIdAsync(id);
            if (department == null)
            {
                throw new BadRequestException($"Department with ID {id} not found.");
            }

            var departmentSetup = new DepartmentSetupOutputDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                ManagerId = department.FKManagerId??0,
                Status = department.RStatus,
                menus = await _menuRepository.GetDepartmentPermittedMenusAsync(id)
            };
            return departmentSetup;
        }
        public async Task<PaginationResponse<DepartmentSettingOutputDto>> GetAllDepartmentsAsync(int companyId, DepartmentSettingInputDto input)
        {
            return await _commonRepository.GetDepartments(companyId, input);
        }
        private async Task<bool> SetMenuPermissionByDepartment(int departmentId, List<RoleSetWithMenuActoinDto> menus)
        {
            // Remove existing permissions for the role
            var existingMappings = await _unitOfWork.Repository<MenuActionDepartmentMappingModel, int>()
                .FindByConditionAsync(s => s.FkDepartmentId == departmentId && s.RStatus == EnumRStatus.Active);
            existingMappings.ToList();

            foreach (var menu in menus)
            {
                var existingMapping = existingMappings.FirstOrDefault(s => s.FKMenuActionMapId == menu.FkMenuActionMapId);
                if (existingMapping != null)
                {
                    existingMapping.IsAllowed = menu.IsAllowed;
                    existingMapping.UpdatedBy = _userInfos.GetCurrentUserId();
                    existingMapping.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.Repository<MenuActionDepartmentMappingModel, int>().UpdateAsync(existingMapping);
                }
                if (existingMapping == null)
                {
                    var newMapping = new MenuActionDepartmentMappingModel
                    {
                        FkDepartmentId = departmentId,
                        FKMenuActionMapId = menu.FkMenuActionMapId,
                        IsAllowed = menu.IsAllowed,
                        CreatedBy = _userInfos.GetCurrentUserId(),
                        CreatedDate = DateTime.UtcNow
                    };
                    await _unitOfWork.Repository<MenuActionDepartmentMappingModel, int>().AddAsync(newMapping);
                }
            }
            return true;
        }

        public async Task<bool> UpdateDepartmentAsync(DepartmentUpdateDto dto)
        {
            try
            {
                var data = await _unitOfWork.Repository<DepartmentModel, int>().FirstOrDefaultAsync(s => s.Id == dto.Id);
                if (data == null)
                    throw new Exception("Department not found");
                data.Description = dto.Description;
                data.FKManagerId = dto.ManagerId;
                await _unitOfWork.Repository<DepartmentModel, int>().UpdateAsync(data);
                if (dto.FKMenuActionIds != null && dto.FKMenuActionIds.Count > 0)
                {
                    await SetMenuPermissionByDepartment(dto.Id, dto.FKMenuActionIds);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating department: " + ex.Message);
            }
            #endregion
        }
        // delete Department
        public async Task<bool> DeleteUserAsync(int id)
        {
            var department = await _unitOfWork.Repository<DepartmentModel, int>().GetByIdAsync(id);
            if (department == null)
                throw new Exception("Department not found");
            department.RStatus = EnumRStatus.Deleted;
            await _unitOfWork.Repository<DepartmentModel, int>().SoftDeleteAsync(department);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
