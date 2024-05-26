using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;

namespace DoctorsApp.Mappers
{
    public class RegisterToDoctor
    {
        Doctor doctor;
        public RegisterToDoctor(RegisterDoctorDTO register)
        {
            doctor=new Doctor();
            doctor.Name=register.Name;
            doctor.Age=register.Age;
            doctor.Qualification=register.Qualification;
            doctor.Experience=register.Experience;
            doctor.Specialty=register.Specialty;
        }
        public Doctor GetDoctor()
        {
            return doctor;
        }
    }
}
