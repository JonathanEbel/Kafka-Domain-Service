using System;

namespace Identity.Dtos
{
    public class NewUserCreatedResponseDto
    {
        public Guid ApplicationUserId { get; set; }
        public bool Active { get; set; }
    }
}
