using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG_Api.Data;
using RPG_Api.DTOs.User;
using RPG_Api.Models;

namespace RPG_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO request)
        {
            var response = await authRepository.Register(new User { UserName = request.Username}, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDTO request)
        {
            var response = await authRepository.Login(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            } 
            return Ok(response);
        }
    }
}