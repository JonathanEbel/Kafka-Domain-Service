using System;

namespace Core
{
    public abstract class EventBase
    {
        public Guid CorrelationId { get; set; }
        public Guid EntityId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
