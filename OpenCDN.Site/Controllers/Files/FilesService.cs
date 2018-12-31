using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OpenCDN.Site.Controllers.Files.Models;

namespace OpenCDN.Site.Controllers.Files
{
    public class FilesService
    {
        public async Task<UploadedFileInfo> GetUploadedFile(string basePath)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UploadedFileInfo>> GetFiles(string basePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetFileContent(string basePath, string fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveFile(string basePath, FileUploadRequest uploadRequest)
        {
            var fullFilePath = $"{basePath}/{uploadRequest.FileId}";
            
            Directory.CreateDirectory(basePath);

            using (var stream = new FileStream($"{basePath}/{uploadRequest.FileId}", FileMode.Create))
            {
                await uploadRequest.File.CopyToAsync(stream);
            }

            // TODO: File was saved, save File Info
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFile(string basePath, string fileId)
        {
            var fullFilePath = $"{basePath}/{fileId}";

            DeletePhysicalFileIfExists(fullFilePath);

            // TODO: File was deleted, delete File Info
            throw new NotImplementedException();
        }
        
        private void DeletePhysicalFileIfExists(string fullFilePath)
        {
            var fileInfo = new FileInfo(fullFilePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}
