using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;

namespace DoctorsApp.Service
{
    public class DoctorService : IDoctorAdminService, IDoctorUserService
    {
        IRepositories<int, Doctor> _repo;
        public DoctorService(IRepositories<int, Doctor> repo)
        {
            _repo= repo;    
        }

        
        /// <summary>
        /// Method to add Doctor
        /// </summary>
        /// <param name="doctor">Object of doctor</param>
        /// <returns>Doctor object</returns>
        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            doctor=await _repo.Add(doctor);
            return doctor;
        }

        public async Task<Doctor> DeleteDoctor(int id)
        {
            var doctor=await GetDoctor(id);
            if (doctor != null)
            {
                doctor =await _repo.Delete(id);
                return doctor;
            }
            throw new NoSuchDoctorException();
        }

        /// <summary>
        /// Method to retrieve doctor by id
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <returns>Object of doctor</returns>
        public async Task<Doctor> GetDoctor(int id)
        {
            var doctor= await _repo.GetAsync(id);
            return doctor;
        }

        /// <summary>
        /// Method to get Doctor
        /// </summary>
        /// <returns>List of Doctor</returns>
        public async Task<List<Doctor>> GetDoctorList()
        {
            var doctor = await _repo.GetAsync();
            return doctor;
        }


        /// <summary>
        /// Method to update doctor's experience
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <param name="experience">Experience in string</param>
        /// <returns>Doctor object</returns>
        public async Task<Doctor> UpdateDoctorExperience(int id, string experience)
        {
            var doctor=await _repo.GetAsync(id);
            if (doctor != null)
            {
                doctor.Experience = experience;
                doctor=await _repo.Update(doctor);
                return doctor;
            }
            return null;
        }

        /// <summary>
        /// Method to update doctor's qualification
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <param name="qualification">Qualification in string</param>
        /// <returns></returns>
        public async Task<Doctor> UpdateDoctorQualification(int id, string qualification)
        {
            var doctor = await _repo.GetAsync(id);
            if (doctor != null)
            {
                doctor.Qualification = qualification;
                doctor = await _repo.Update(doctor);
                return doctor;
            }
            return null;
        }
    }
}
