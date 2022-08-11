using Application.DTO.Responses;
using Application.DTO;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword
{
    public class NewPasswordCommand : IRequest<ResetPasswordResponse>
    {
        public ResetPasswordModel model { get; set; } = new ResetPasswordModel();
    }
}
