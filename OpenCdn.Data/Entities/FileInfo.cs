using System;

namespace OpenCdn.Data.Entities
{
    public class FileInfo
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
