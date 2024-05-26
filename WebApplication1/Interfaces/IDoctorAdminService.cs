using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IDoctorAdminService
    {
        public Task<List<Doctor>> GetDoctorList();
        public Task<Doctor> AddDoctor(Doctor doctor);
        public Task<Doctor> UpdateDoctorQualification(int id, string qualification);
        public Task<Doctor> UpdateDoctorExperience(int id,string experience);
        public Task<Doctor> DeleteDoctor(int id);

    }
}
