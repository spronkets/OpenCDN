using System.ComponentModel.DataAnnotations;

namespace OpenCdn.Data.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
    }
}
