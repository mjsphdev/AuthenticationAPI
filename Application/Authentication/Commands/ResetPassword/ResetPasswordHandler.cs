using Domain;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Authentication.Services;
using Application.DTO.Responses;

namespace Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserDbContext _userDbContext;

        public ResetPasswordHandler(
                UserManager<User> userManager,
                UserDbContext userDbContext
            )
        {
            _userManager = userManager;
            _userDbContext = userDbContext;

        }
        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.model.ResetEmail);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            Random rnd = new Random();
            int otp = rnd.Next(1000, 9999);

            var resetPassword = new Domain.ResetPassword()
            {
                Email = command.model.ResetEmail,
                OTP = otp.ToString(),
                Token = token,
                UserId = user.Id,
                InsertDateTime = DateTime.Now
            };

            // Save data into db with OTP
            await _userDbContext.AddAsync(resetPassword);
            await _userDbContext.SaveChangesAsync();

            await EmailSender.SendEmailAsync(command.model.ResetEmail, "Reset Password OTP", "Hello "
                + command.model.ResetEmail + "<br><br>Please find the reset password token below<br><br><b>"
                + otp + "<b><br><br>Thanks<br>oktests.com");

            return new ResetPasswordResponse { Status="200", Message = "OTP has been sent to your email."};
        }
    }
}
