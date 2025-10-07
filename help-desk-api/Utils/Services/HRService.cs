using Models.Dto.Intregation;
using Models.Enum;
using Newtonsoft.Json;

using Utils.Integration.API;
using Utils.Integration.Request;

namespace Utils.Integration
{
    public interface IHRService
    {
        Task<List<UserWithEmployeeDto>> GetUsersAsync();
        Task<List<DepartmentOutputDto>> GetDepartmentsAsync();
    }

    public class HRService : IHRService
    {
        private readonly IQsClient _qsClient;

        public HRService(IQsClient qsClient)
        {
            _qsClient = qsClient ?? throw new ArgumentNullException(nameof(qsClient));
        }

        public async Task<List<DepartmentOutputDto>> GetDepartmentsAsync()
        {
            var request = new DepartmentServiceRequest(HttpMethod.Get, "api/company-data/department");
            try
            {
                dynamic response = await _qsClient.RequestAsync<object>(request, EnumExternalServiceModules.HRAPI);
                if (response == null)
                    throw new InvalidOperationException("No department data returned from HRAPI.");

                var departmentsJson = JsonConvert.SerializeObject(response);
                var departments = JsonConvert.DeserializeObject<List<DepartmentOutputDto>>(departmentsJson);

                return departments ?? new List<DepartmentOutputDto>();
            }
            catch (Exception ex)
            {
                // Log exception here if logging is available
                throw new ApplicationException("Error fetching departments.", ex);
            }
        }

        public async Task<List<UserWithEmployeeDto>> GetUsersAsync()
        {
            var request = new DepartmentServiceRequest(HttpMethod.Get, "api/company-data/users");
            try
            {
                dynamic response = await _qsClient.RequestAsync<object>(request, EnumExternalServiceModules.HRAPI);
                if (response == null)
                    throw new InvalidOperationException("No user data returned from HRAPI.");

                var usersJson = JsonConvert.SerializeObject(response);
                var users = JsonConvert.DeserializeObject<List<UserWithEmployeeDto>>(usersJson);

                return users ?? new List<UserWithEmployeeDto>();
            }
            catch (Exception ex)
            {
                // Log exception here if logging is available
                throw new ApplicationException("Error fetching users.", ex);
            }
        }
    }
}
