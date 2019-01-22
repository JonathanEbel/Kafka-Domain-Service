using System;
using System.Collections.Generic;

namespace Identity.Dtos
{
    public class IdentityLoginResponseDto
    {
        public Guid IdentityUserId { get; set; }
        public string TokenSubject { get; set; }
        public List<IdentityClaimDto> Claims { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
