using Identity.Events;
using Projections.Domain.Repos;

namespace Projections.Domain.EventHandlers.Implementations
{
    public class HandleUserLoggedInEvent : IHandleUserLoggedInEvent
    {
        private readonly IUsageRepository _usageRepository;

        public HandleUserLoggedInEvent(IUsageRepository usageRepository)
        {
            _usageRepository = usageRepository;
        }

        public void Handle(UserLoggedInEvent ev)
        {
            var usage = _usageRepository.GetByIdentityId(ev.EntityId);

            if (usage != null)
            {
                usage.UpdateLastLogin(ev.TimeStamp);
                _usageRepository.Save();
            }
        }
    }
}
