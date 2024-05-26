using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IPatientUserService
    {
        public Task<Patient> GetPatient(int id);
    }
}
