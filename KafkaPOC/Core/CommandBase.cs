using System;

namespace Core
{
    public abstract class CommandBase
    {
        public Guid? CommandId { get; set; }
        public Guid? EntityId { get; set; }
    }
}
