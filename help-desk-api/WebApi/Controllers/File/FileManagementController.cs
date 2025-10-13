using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Dashboard;
using Models.Dto.GlobalDto;
using Models.Dto.Ticket.Models.Dto.Tickets;
using Services.File;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("file")]
    [AllowAnonymous]
    public class FileManagementController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileManagementController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public  IActionResult Save([FromForm] List<IFormFile> fileList)
        {
            return Ok(_fileService.SaveFile(fileList));
        }

    }
}