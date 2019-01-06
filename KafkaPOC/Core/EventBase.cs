using System;

namespace Core
{
    public abstract class EventBase
    {
        public Guid CorrelationId { get; set; }
    }
}
