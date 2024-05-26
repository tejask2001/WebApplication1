using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IPatientAdminService
    {
        public Task<List<Patient>> GetPatientList();
        public Task<Patient> AddPatient(Patient patient);
        public Task<Patient> UpdatePatientMobile(int id, string mobile);
        public Task<Patient> UpdatePatientAge(int id, int age);
        public Task<Patient> DeletePatient(int id);
    }
}
