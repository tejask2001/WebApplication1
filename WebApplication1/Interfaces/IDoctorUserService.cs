using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IDoctorUserService
    {
        public Task<Doctor> GetDoctor(int id);
    }
}
