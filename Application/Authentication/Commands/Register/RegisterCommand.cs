using Application.Authentication.Common;
using Application.DTO;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommand : IRequest<Response>
    {
        public RegisterModel model { get; set; } = new RegisterModel();
    }
}
