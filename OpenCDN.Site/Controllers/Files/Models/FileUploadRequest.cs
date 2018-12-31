using Microsoft.AspNetCore.Http;

namespace OpenCDN.Site.Controllers.Files.Models
{
    public class FileUploadRequest
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
