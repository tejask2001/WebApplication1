using DoctorsApp.Models.DTOs;

namespace DoctorsApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(LoginUserDTO user);
    }
}
