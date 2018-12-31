using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using OpenCDN.Site.Controllers.Files.Models;

namespace OpenCDN.Site.Controllers.Files
{
    [Route("api/files")]
    public class FilesController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMemoryCache memoryCache;
        private readonly FilesService filesService;

        private string basePath => $"{hostingEnvironment.WebRootPath}/uploads/";

        public FilesController(IHostingEnvironment hostingEnvironment, IMemoryCache memoryCache, FilesService filesService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.memoryCache = memoryCache;
            this.filesService = filesService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(string fileId = null, string fileName = null, string contentType = null, string description = null)
        {
            if (Request.Form.Files.Count != 1)
            {
                return BadRequest("This endpoint requires 1 file.");
            }

            var file = Request.Form.Files.Single();
            fileName = !string.IsNullOrEmpty(fileName) ? fileName : file.FileName;
            fileId = !string.IsNullOrEmpty(fileId) ? fileId : fileName.Replace(" ", "_");
            contentType = !string.IsNullOrEmpty(contentType) ? contentType : file.ContentType;

            var saveFileRequest = new FileUploadRequest
            {
                FileId = fileId,
                FileName = fileName,
                ContentType = contentType,
                File = file,
                Description = description
            };

            var fileSaved = await filesService.SaveFile(basePath, saveFileRequest);
            if (fileSaved)
            {
                return Ok("The file was uploaded.");
            }
            return Conflict("There was a problem uploading the file.");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFileContent(string fileId)
        {
            if (!memoryCache.TryGetValue(fileId, out string cacheEntry))
            {
                var fileContent = await filesService.GetFileContent(basePath, fileId);
                if (fileContent == null)
                {
                    return NotFound("The file wasn't found.");
                }
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                memoryCache.Set(fileId, cacheEntry, cacheEntryOptions);
            }
            return Ok(memoryCache);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var uploadedFile = await filesService.GetUploadedFile(fileId);
            if (uploadedFile == null)
            {
                return NotFound("The file wasn't found.");
            }
            var result = PhysicalFile(basePath, uploadedFile.ContentType, uploadedFile.FileName);
            return result;
        }
    }
}
