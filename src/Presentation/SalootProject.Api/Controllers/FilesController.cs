using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.Midllewares;
using Services.Files.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SalootProject.Api.Controllers
{
    public class FilesController : BaseController
    {
        #region Fields

        private readonly IFileService _fileService;

        #endregion Fields

        #region Ctor

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        #endregion Ctor

        #region Actions

        [HttpPost, Authorize(Roles = "Admin,User")]
        public async Task<ApiResponse<object>> StoreFile(IFormFile formFile, string description, CancellationToken cancellationToken)
        {
            return Ok(new { Id = await _fileService.StoreFileAsync(formFile, description, cancellationToken) });
        }

        [HttpGet("{id:int:min(1)}"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> GetFileById(int id, CancellationToken cancellationToken)
        {
            var file = await _fileService.GetFileByIdAsync(id, cancellationToken);
            return File(file.FileStream, file.ContentType, file.FileDownloadName);
        }


        [HttpDelete("{id:int:min(1)}"), Authorize(Roles = "Admin,User")]
        public async Task<ApiResponse> DeleteFile(int id, CancellationToken cancellationToken)
        {
            await _fileService.DeleteFileAsync(id, cancellationToken);
            return Ok();
        }

        #endregion Actions
    }
}