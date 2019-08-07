using System;
using System.Collections.Generic;

namespace OpenCdn.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
