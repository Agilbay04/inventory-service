using Microsoft.AspNetCore.Mvc;
using System.Net;
using InventoryService.Domain.File.Services;
using InventoryService.Infrastructure.Helpers;
using InventoryService.Domain.File.Dto;

namespace InventoryService.Http.API.Version1.File.Controllers
{
    [Route("api/v1/files")]
    [ApiController]
    public class FileController(FileService FileService) : ControllerBase
    {
        private readonly FileService _fileService = FileService;

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ApiResponseData<FileDto>> UploadAsync([FromForm] FileUploadDto request)
        {
            var data = await _fileService.UploadAsync(request);
            return new ApiResponseData<FileDto>(HttpStatusCode.OK, data);
        }

        [HttpPost("download")]
        public ApiResponseData<FileDto> Download([FromBody] FileDowloadDto request)
        {
            var data = _fileService.Download(request);
            return new ApiResponseData<FileDto>(HttpStatusCode.OK, data);
        }
    }
}
