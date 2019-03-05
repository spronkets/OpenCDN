using OpenCdn.WebApi.Controllers.Files.Models;

namespace OpenCdn.WebApi.Controllers.Files.Extensions
{
    public static class FileExtensions
    {
        public static bool IsValidRequest(this FileUploadRequest uploadRequest)
        {
            return !string.IsNullOrWhiteSpace(uploadRequest.FileId) && uploadRequest.File != null;
        }
    }
}
