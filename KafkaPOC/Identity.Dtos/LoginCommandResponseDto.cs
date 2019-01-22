using System;

namespace Identity.Dtos
{
    public class LoginCommandResponseDto
    {
        public bool LoginSuccess { get; set; }
        public string FailureReason { get; set; }
        public Guid IdentityUserId { get; set; }
    }
}
