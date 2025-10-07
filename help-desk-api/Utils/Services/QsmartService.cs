using Models.Dto.GlobalDto;
using Models.Dto.Intregation;
using Models.Enum;
using Newtonsoft.Json;
using Utils.Integration.API;
using Utils.Integration.Request;

namespace Utils.Services
{
    public interface IQSmartService
    {
        Task<List<CustomerDetailsOutPutDto>> GetCustomers();
        Task<List<QMSProjectDetailDto>> GetProjects();
        Task<List<DropdownOutputDto<int, string>>> GetSchemes();
    }
    public class QSmartService: IQSmartService
    {
        private readonly IQsClient _qsClient;

        public QSmartService(IQsClient qsClient)
        {
            _qsClient = qsClient;
        }
        public async Task<List<CustomerDetailsOutPutDto>> GetCustomers()
        {
            var request = new DepartmentServiceRequest(HttpMethod.Get, "api/company-data/customers");
            try
            {
                dynamic response = await _qsClient.RequestAsync<object>(request, EnumExternalServiceModules.QsmartAPI);
                if (response == null)
                    throw new InvalidOperationException("No department data returned from HRAPI.");

                var departmentsJson = JsonConvert.SerializeObject(response);
                var departments = JsonConvert.DeserializeObject<List<CustomerDetailsOutPutDto>>(departmentsJson);

                return departments ?? new List<CustomerDetailsOutPutDto>();
            }
            catch (Exception ex)
            {
                // Log exception here if logging is available
                throw new ApplicationException("Error fetching departments.", ex);
            }
        }
        public async Task<List<DropdownOutputDto<int, string>>> GetSchemes()
        {
            var request = new DepartmentServiceRequest(HttpMethod.Get, "api/company-data/schemes");
            try
            {
                dynamic response = await _qsClient.RequestAsync<object>(request, EnumExternalServiceModules.QsmartAPI);
                if (response == null)
                    throw new InvalidOperationException("No department data returned from HRAPI.");

                var departmentsJson = JsonConvert.SerializeObject(response);
                var departments = JsonConvert.DeserializeObject<List<DropdownOutputDto<int,string>>>(departmentsJson);

                return departments ?? new List<DropdownOutputDto<int, string>>();
            }
            catch (Exception ex)
            {
                // Log exception here if logging is available
                throw new ApplicationException("Error fetching departments.", ex);
            }
        }
        public async Task<List<QMSProjectDetailDto>> GetProjects()
        {
            var request = new DepartmentServiceRequest(HttpMethod.Get, "api/company-data/projects");
            try
            {
                dynamic response = await _qsClient.RequestAsync<object>(request, EnumExternalServiceModules.QsmartAPI);
                if (response == null)
                    throw new InvalidOperationException("No department data returned from HRAPI.");

                var departmentsJson = JsonConvert.SerializeObject(response);
                var departments = JsonConvert.DeserializeObject<List<QMSProjectDetailDto>>(departmentsJson);

                return departments ?? new List<QMSProjectDetailDto>();
            }
            catch (Exception ex)
            {
                // Log exception here if logging is available
                throw new ApplicationException("Error fetching departments.", ex);
            }
        }
    }
}
