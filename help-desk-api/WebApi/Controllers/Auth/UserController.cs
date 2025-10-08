using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.GlobalDto;
using Models.Dto.UserManagement;
using Models.Entities.UserManagement;
using Models.Enum;
using Services.UserManagement;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("user")]
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
        public async Task<ActionResult> Add(UserModel user)
        {
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UserModel user)
        {
            if (id != user.Id) return BadRequest();
            await _userService.UpdateAsync(user);
            return NoContent();
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



    }
}
