using DoctorsApp.Models.DTOs;

namespace DoctorsApp.Interfaces
{
    public interface IUserService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> Register(RegisterDoctorDTO user);
        public Task<LoginUserDTO> Register(RegisterPatientDTO user);
    }
}