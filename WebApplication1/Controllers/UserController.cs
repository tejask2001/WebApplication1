using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<LoginUserDTO> Register(RegisterDoctorDTO register)
        {
            var result = await _userService.Register(register);
            return result;
        }

        [Route("RegisterPatient")]
        [HttpPost]
        public async Task<LoginUserDTO> RegisterPatient(RegisterPatientDTO register)
        {
            var result = await _userService.Register(register);
            return result;
        }

        [Route("Login")]
        [HttpPost]
        [EnableCors("ReactPolicy")]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO user)
        {
            try
            {
                var result = await _userService.Login(user);
                return Ok(result);
            }
            catch(InvalidUserException iue)
            {
                _logger.LogCritical(iue.Message);
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
