using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorsApp.Service
{
    public class PatientService : IPatientAdminService, IPatientUserService
    {
        IRepositories<int,Patient> _repository;
        public PatientService(IRepositories<int, Patient> repository)
        {
            _repository=repository;
        }

        /// <summary>
        /// Method to add Patient
        /// </summary>
        /// <param name="patient">Object of patient</param>
        /// <returns>Patient object</returns>
        public async Task<Patient> AddPatient(Patient patient)
        {
            patient=await _repository.Add(patient);
            return patient;
        }

        public async Task<Patient> DeletePatient(int id)
        {
            var patient = await GetPatient(id);
            if(patient!=null)
            {
                patient = await _repository.Delete(id);
                return patient;
            }
            throw new NoSuchPatientException();
        }

        /// <summary>
        /// Method to Get Patient by Id
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <returns>Petient object</returns>
        public async Task<Patient> GetPatient(int id)
        {
            var patient= await _repository.GetAsync(id);
            return patient;
        }

        /// <summary>
        /// Method to get Patients
        /// </summary>
        /// <returns>List of Patients</returns>
        public async Task<List<Patient>> GetPatientList()
        {
            var patients = await _repository.GetAsync();
            return patients;
        }

        public async Task<Patient> UpdatePatientAge(int id, int age)
        {
            var patient = await _repository.GetAsync(id);
            if(patient!=null)
            {
                patient.Age = age;
                _repository.Update(patient);
                return patient;
            }
            return null;
        }

        
        /// <summary>
        /// Method to update Patient
        /// </summary>
        /// <param name="id">id in int</param>
        /// <param name="mobile">Mobile in string</param>
        /// <returns>Patient object</returns>
        public async Task<Patient> UpdatePatientMobile(int id, string mobile)
        {
            var patient = await _repository.GetAsync(id);
            if (patient != null)
            {
                patient.MobileNumber = mobile;
                _repository.Update(patient);
                return patient;
            }
            return null;
        }
    }
}
