using Models.Dto.GlobalDto;
namespace Models.Dto.UserManagement
{
    public class UserFilterDto : PagedInputDto
    {
        public int CompanyId { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
        public List<int> DepartmentIds { get; set; } = new List<int>();
    }
}
