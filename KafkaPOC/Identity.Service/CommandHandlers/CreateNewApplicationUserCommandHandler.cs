using Identity.Commands;
using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;
using Identity.Models;

namespace Identity.Service.CommandHandlers
{
    public class CreateNewApplicationUserCommandHandler : ICreateNewApplicationUserCommandHandler
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public CreateNewApplicationUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public ApplicationUser Handle(CreateNewApplicationUserCommand cmd)
        {
            var applicationUser = new ApplicationUser(cmd.UserName, cmd.Password, cmd.ConfirmPassword, false, true);

            foreach (var role in cmd.Roles)
                applicationUser.AddClaim("role", role);

            _applicationUserRepository.Add(applicationUser);
            _applicationUserRepository.Save();

            //fire event here...

            return applicationUser;
        }
    }
}
