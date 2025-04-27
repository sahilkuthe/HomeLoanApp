using HomeLoanAPI.DTOs;
using HomeLoanAPI.Helpers;
using HomeLoanAPI.Models;
using HomeLoanAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtHelper = new JwtHelper(configuration);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (await _userRepository.UserExists(registerDTO.Email))
                return BadRequest("User already exists!");

            var user = new User
            {
                FirstName = registerDTO.FirstName,
                MiddleName = registerDTO.MiddleName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                PhoneNumber = registerDTO.PhoneNumber,
                Dob = registerDTO.Dob,
                Gender = registerDTO.Gender,
                Nationality = registerDTO.Nationality,
                AadharNo = registerDTO.AadharNo,
                PanNo = registerDTO.PanNo
            };

            var createdUser = await _userRepository.Register(user);

            return Ok(new { Message = "Registration Successful!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.Login(loginDTO.Email, loginDTO.Password);

            if (user == null)
                return Unauthorized("Invalid credentials!");

            var token = _jwtHelper.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }
    }
}
