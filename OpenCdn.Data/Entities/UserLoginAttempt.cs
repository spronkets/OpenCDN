using System;

namespace OpenCdn.Data.Entities
{
    public class UserLoginAttempt
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ClientIp { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}
