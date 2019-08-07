using System;

namespace OpenCdn.Data.Entities
{
    public class FileGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HomeUrl { get; set; }
        public string RepositoryUrl { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        //TODO: FileGroup Tags, public List<string> Tags { get; set; } = new List<string>();
        //TODO: FileGroup Authors, public List<string> Authors { get; set; } = new List<string>();
    }
}
