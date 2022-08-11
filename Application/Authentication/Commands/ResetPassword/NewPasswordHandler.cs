using Domain;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.DTO.Responses;

namespace Application.Authentication.Commands.ResetPassword
{
    public class NewPasswordHandler : IRequestHandler<NewPasswordCommand, ResetPasswordResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserDbContext _userDbContext;


        public NewPasswordHandler(
                UserManager<User> userManager,
                UserDbContext userDbContext
            )
        {
            _userManager = userManager;
            _userDbContext = userDbContext;
        }
        public async Task<ResetPasswordResponse> Handle(NewPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.model.Email);

            if (user == null) 
                return new ResetPasswordResponse { Status = "400", Message = "Invalid Email" };


            // getting token from otp
            var resetPasswordDetails = await _userDbContext.ResetPassword
                .Where(rp => rp.OTP == command.model.otp && rp.UserId == user.Id)
                .OrderByDescending(rp => rp.InsertDateTime)
                .FirstOrDefaultAsync();

            if (resetPasswordDetails == null) 
                return new ResetPasswordResponse { Status = "400", Message = "Invalid OTP" };

            // Verify if token is older than 15 minutes
            var expirationDateTime = resetPasswordDetails.InsertDateTime.AddMinutes(15);
            if (expirationDateTime < DateTime.Now) 
                return new ResetPasswordResponse { Status = "400", Message = "OTP expired, pls generate new OTP" };


            var res = await _userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, command.model.newPassword);

            return new ResetPasswordResponse { Status="200", Message="Password succesfully changed."};
        }
    }
}
