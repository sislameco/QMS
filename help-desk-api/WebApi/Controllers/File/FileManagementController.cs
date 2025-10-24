using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Dashboard;
using Models.Dto.GlobalDto;
using Services.File;

namespace WebApi.Controllers.IssueManagement
{
    [ApiController]
    [Route("file")]
    public class FileManagementController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileManagementController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] List<IFormFile> fileList) 
        {
            return Ok(await _fileService.SaveFile(fileList));
        } 

    }
}