using System;

namespace Core
{
    public interface IEventHandler<T> : IDisposable
    {
        void HandleEvent(T ev);
    }
}
