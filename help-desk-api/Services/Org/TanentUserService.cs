using Microsoft.AspNetCore.Identity;
using Models.Dto.Org;
using Models.Dto.Pagination;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using Repository.Repo.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Services.Org
{
    public interface ITenantUserService
    {
        // scan UserSetupController and implement similar methods
        Task<PaginationResponse<UserSetupOutputDto>> GetUsersByTenantAsync(int companyId, UserPaginationInputDto input);
        Task<UserSetupOutputDto> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(UserSetupInputDto input);
        Task<bool> DeleteUserAsync(int userId);
        Task<UserTilesDto> GetUserTilesAsync(int companyId, UserPaginationInputDto input);
    }
    public class TenantUserService : ITenantUserService
    {

        private readonly IUserMGtRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public TenantUserService(IUserMGtRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponse<UserSetupOutputDto>> GetUsersByTenantAsync(int companyId, UserPaginationInputDto input)
        {
            return await _userRepository.GetTenentUser(companyId, input);
        }

        public async Task<UserSetupOutputDto> GetUserByIdAsync(int userId)
        {
            return _userRepository.GetTenentUserById(userId);
        }

        public async Task<bool> UpdateUserAsync(UserSetupInputDto input)
        {
            try
            {
                var user = await _unitOfWork.Repository<UserModel, int>().FirstOrDefaultAsync(s => s.Id == input.Id);
                if (user == null)
                    throw new BadRequestException("User not found");

                user.FkDepartmentId = input.DepartmentId;
                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
                user.FullName = $"{input.FirstName} {input.LastName}";
                user.Email = input.EmailAddress;
                user.Phone = input.PhoneNumber;

                var userRole = await _unitOfWork.Repository<UserRoleModel, int>().FirstOrDefaultAsync(s => s.FKRoleId == input.RoleId && s.FKUserId == input.Id);
                if (userRole == null)
                {
                    var newUserRole = new UserRoleModel
                    {
                        FKRoleId = input.RoleId,
                        FKUserId = input.Id,
                        RStatus = EnumRStatus.Active
                    };
                    await _unitOfWork.Repository<UserRoleModel, int>().AddAsync(newUserRole);
                }
                else
                {
                    userRole.FKRoleId = input.RoleId;
                    _unitOfWork.Repository<UserRoleModel, int>().Update(userRole);
                }


                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                await _unitOfWork.Repository<UserModel, int>().DeleteAsync(userId);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<UserTilesDto> GetUserTilesAsync(int companyId, UserPaginationInputDto input)
        {
            return new UserTilesDto
            {
                 ActiveUsers = 10,
                 Admins = 5,
                 Departments = 5,
                 TotalUsers = 50

            };
        }
    }

}
