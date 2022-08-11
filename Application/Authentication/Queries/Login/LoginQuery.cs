using Application.DTO;
using Application.DTO.Responses;
using ErrorOr;
using MediatR;
using Application.Authentication.Common;

namespace Application.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginModel model { get; set; } = new LoginModel();
    }
}
