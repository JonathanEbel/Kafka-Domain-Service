using Identity.Events;

namespace Projections.Domain.EventHandlers
{
    public interface IHandleUserLoggedInEvent
    {
        void Handle(UserLoggedInEvent ev);
    }
}
