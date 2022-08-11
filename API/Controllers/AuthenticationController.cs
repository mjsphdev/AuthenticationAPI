using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.RefreshToken;
using Application.Authentication.Queries.Login;
using Application.DTO;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _userManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IMediator mediator, 
            SignInManager<User> userManager,
            ILogger<AuthenticationController> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            var h = StatusCode(StatusCodes.Status200OK);
            return Ok(h);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var h = HttpContext.Request.Headers;
            return Ok(h);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var h = StatusCode(StatusCodes.Status200OK);
            return Ok(h);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogWarning($"User {model.Username} trying to login");

            if (ModelState.IsValid)
            {
                var query = new LoginQuery() { model = model };
                var authResult = await _mediator.Send(query);
                return Ok(authResult);
            }

            return Unauthorized();

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogWarning($"Registration for {model.Username}");
            if (ModelState.IsValid)
            {
                var command = new RegisterCommand() { model = model };

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            
            return BadRequest(ModelState);

        }

        [HttpPost("send-password-reset-code")]
        public async Task<IActionResult> SendPasswordResetCode([FromBody] SendOTP model)
        {
            _logger.LogWarning($"{model.ResetEmail} requested for an OTP");
            if (ModelState.IsValid)
            {
                var command = new ResetPasswordCommand() { model = model };
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            return BadRequest(ModelState);


        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            _logger.LogWarning($"{model.Email} trying to reset password");
            if (ModelState.IsValid)
            {
                var command = new NewPasswordCommand() { model = model };
                var result = await _mediator.Send(command);

                return Ok(result);
            }

            return BadRequest(ModelState);


        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            _logger.LogWarning($"{tokenModel.RefreshToken} trying to generate new access token");
            if (tokenModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var command = new RefreshTokenCommand() { refeshTokenModel = tokenModel };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();
            return Ok("Logout");
        }



    }
}
