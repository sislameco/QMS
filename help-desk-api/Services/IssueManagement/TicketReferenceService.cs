using Models.Dto.GlobalDto;
using Models.Dto.Org;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Entities.Setup;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;


namespace Services.IssueManagement
{
    public interface ITicketReferenceService
    {
        List<DropdownOutputDto<int,string>> GetTickets(int fkCompanyId);
        List<DropdownOutputDto<int, string>> GetDepartments(int fkCompanyId);
        List<TicketTypeDDL> GetTicketTypes(int fkCompanyId);
        List<DropdownOutputDto<int, string>> GetRootCauses(int fkCompanyId);
        List<DropdownOutputDto<int, string>> GetRelocations(int fkCompanyId);
        List<CustomerOutputDto> GetCustomers(int fkCompanyId);
        List<ProjectOutputDto> GetProjects(int fkCompanyId);
        List<DropdownOutputDto<int, string>> GetUsers(int fkCompanyId);
        List<FieldOutputDto> GetSubforms(int ticketTypeId);
    }
    public class TicketReferenceService : ITicketReferenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketReferenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DropdownOutputDto<int, string>> GetTickets(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<TicketModel,int>().FindByConditionOneColumn(x=> x.FKCompanyId == fkCompanyId && x.RStatus == EnumRStatus.Active,x=> new {x.Id,x.TicketNumber});

            return data
                .Select(t => new DropdownOutputDto<int,string>
                {
                    Id = t.Id,
                    Name = t.TicketNumber,
                    // Map other properties as needed
                })
                .ToList();
        }

        public List<DropdownOutputDto<int, string>> GetDepartments(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<DepartmentModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                    x => new { x.Id, x.Name }
                );

            return data
                .Select(d => new DropdownOutputDto<int, string>
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
        }

        public List<TicketTypeDDL> GetTicketTypes(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<TicketTypeModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                    x => new { x.Id, x.Title , x.QmsType, x.Priority}
                );

            return data
                .Select(tt => new TicketTypeDDL
                {
                    Id = tt.Id,
                    Title = tt.Title,
                     QmsType = tt.QmsType,
                     Priority = tt.Priority
                })
                .ToList();
        }

        public List<DropdownOutputDto<int, string>> GetRootCauses(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                    x => new { x.Id, x.Name }
                );

            return data
                .Select(rc => new DropdownOutputDto<int, string>
                {
                    Id = rc.Id,
                    Name = rc.Name
                })
                .ToList();
        }

        public List<DropdownOutputDto<int, string>> GetRelocations(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<CompanyDefineRootResolutionModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                    x => new { x.Id, x.Name }
                );

            return data
                .Select(r => new DropdownOutputDto<int, string>
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
        }

        public List<CustomerOutputDto> GetCustomers(int fkCompanyId)
        {
            var customers = _unitOfWork.Repository<CompanyCustomerModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                    x => new { x.Id, x.CustomerFirstName, x.CustomerLastName, x.Email, x.Phone }
                );

            return customers
                .Select(c => new CustomerOutputDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    Phone = c.Phone,
                    FullName = string.Concat(c.CustomerFirstName, " ", c.CustomerLastName)
                }).ToList();
        }

        public List<ProjectOutputDto> GetProjects(int fkCompanyId)
        {
            var projects = _unitOfWork.Repository<CompanyProjectModel, int>()
            .FindByConditionOneColumn(
                x => x.RStatus == EnumRStatus.Active && x.FKCompanyId == fkCompanyId,
                x => new { x.Id, x.ReferenceNumber, x.ProjectAddress}
            );

            return projects
                .Select(p => new ProjectOutputDto
                {
                    Id = p.Id,
                    ProjectAddress = p.ProjectAddress,
                    ReferenceNumber = p.ReferenceNumber,
                }).ToList();
        }

        public List<DropdownOutputDto<int, string>> GetUsers(int fkCompanyId)
        {
            var data = _unitOfWork.Repository<UserModel, int>()
                .FindByConditionOneColumn(
                    x => x.RStatus == EnumRStatus.Active && x.FkCompanyId == fkCompanyId,
                    x => new { x.Id, Name = string.Concat(x.FirstName, " ", x.LastName) }
                );

            return data
                .Select(u => new DropdownOutputDto<int, string>
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
        }

        public List<FieldOutputDto> GetSubforms(int ticketTypeId)
        {
            var data = _unitOfWork.Repository<CustomFieldModel,int>().FindByConditionOneColumn(x=> x.FkTicketTypeId == ticketTypeId && x.RStatus == EnumRStatus.Active, x=> new { x.IsRequired, x.DisplayName, x.DataType, x.Id, x.DDLValue })
                .Select(s => new FieldOutputDto
                {
                    Id = s.Id,
                    DisplayName = s.DisplayName,
                    DataType = s.DataType,
                    IsRequired = s.IsRequired,
                    DDLValue = s.DDLValue
                }).ToList();
            return data;
        }
    }
}