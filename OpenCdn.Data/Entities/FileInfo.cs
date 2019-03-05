using System;
using System.ComponentModel.DataAnnotations;

namespace OpenCdn.Data.Entities
{
    public class FileInfo
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
