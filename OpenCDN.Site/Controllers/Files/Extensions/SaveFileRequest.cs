using OpenCDN.Site.Controllers.Files.Models;

namespace OpenCDN.Site.Controllers.Files.Extensions
{
    public static class FileExtensions
    {
        public static bool IsValidRequest(this FileUploadRequest uploadRequest)
        {
            return !string.IsNullOrWhiteSpace(uploadRequest.FileId) && uploadRequest.File != null;
        }
    }
}
