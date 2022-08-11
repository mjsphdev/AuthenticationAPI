using Application.DTO;
using Application.DTO.Responses;
using MediatR;

namespace Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public TokenModel refeshTokenModel { get; set; } = new TokenModel();
    }
}
