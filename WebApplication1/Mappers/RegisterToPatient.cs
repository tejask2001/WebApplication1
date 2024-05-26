using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;

namespace DoctorsApp.Mappers
{
    public class RegisterToPatient
    {
        Patient patient;
        public RegisterToPatient(RegisterPatientDTO register)
        {
            patient = new Patient();
            patient.Name = register.Name;
            patient.Age = register.Age;
            patient.Gender = register.Gender;
            patient.MobileNumber = register.MobileNumber;
        }
        public Patient GetPatient()
        {
            return patient;
        }
    }
}
