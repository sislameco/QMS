using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.GlobalDto;
using Models.Dto.Org;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Services.UserManagement;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("user")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll([FromForm] UserFilterDto input)
        {
            var users = await _userService.GetAllAsync(input);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Add(HostUserInputDto user)
        {
            await _userService.AddAsync(user);
            return Ok(true);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, HostUserUpdateInputDto user)
        {
            await _userService.UpdateAsync(id ,user);
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
        // generate user dropdown endpoint 
        /// <summary>
        /// Returns a list of users for dropdowns (Id & FullName only).
        /// </summary>
        [HttpGet("dropdown")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDropdownDto>>> GetUserDropdown(int companyId)
        {
            var users = await _userService.GetUserSelectedList(companyId);
            return Ok(users);
        }

        /// <summary>
        /// Sends an invitation email to the specified user.
        /// </summary>
        [HttpPost("{userId}/send-invitation")]
        public async Task<ActionResult> SendInvitationEmail(int userId)
        {
            return Ok(await _userService.SendInvitation(userId));
        }

        [HttpPost("{userId}/request-accept")]
        public async Task<ActionResult> RequestAccept(int userId)
        {
            return Ok(await _userService.AcceptRequest(userId));
        }



    }
}
