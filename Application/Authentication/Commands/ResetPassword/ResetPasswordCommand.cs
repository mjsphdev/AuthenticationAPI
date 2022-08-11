using Application.DTO.Responses;
using Application.DTO;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
    {
        public SendOTP model { get; set; } = new SendOTP();
    }
}
