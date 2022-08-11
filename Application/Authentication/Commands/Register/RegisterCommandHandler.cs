using Application.DTO;
using MediatR;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response>
    {
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(
                UserManager<User> userManager
            )
        {
            _userManager = userManager;
        }

        public async Task<Response> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            User user = new()
            {
                Email = command.model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = command.model.Username
            };

            var result = await _userManager.CreateAsync(user, command.model.Password);

            if (result.Succeeded)
                return new Response { Status = "Success", Message = "Registered Succesfully" };

            return new Response { Status = "Error", Errors = result.Errors };
                
        }

    }
}
