using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoctorsApp.Repositories
{
    public class DoctorRepository : IRepositories<int, Doctor>
    {
        RequestTrackerContext _context;
        ILogger<DoctorRepository> _logger;

        public DoctorRepository(RequestTrackerContext context, ILogger<DoctorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Method to add Doctor
        /// </summary>
        /// <param name="item">Object of doctor</param>
        /// <returns>Doctor object</returns>
        public async Task<Doctor> Add(Doctor item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Doctor added " + item.Id);
            return item;
        }

        /// <summary>
        /// Method to Delete Doctor
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Object of Doctor</returns>
        public async Task<Doctor> Delete(int key)
        {
            var doctor = await GetAsync(key);
            _context?.Doctors.Remove(doctor);
            _context?.SaveChanges();
            _logger.LogInformation("Doctor deleted " + key);
            return doctor;
        }

        /// <summary>
        /// Method to retrieve doctor by id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Object of doctor</returns>
        /// <exception cref="NoSuchDoctorException">Throws when no doctor with given id found</exception>
        public async Task<Doctor> GetAsync(int key)
        {
            var doctors = await GetAsync();
            var doctor= doctors.FirstOrDefault(e=>e.Id==key);
            if(doctor != null)
            {
                return doctor;
            }
            throw new NoSuchDoctorException();
        }

        /// <summary>
        /// Method to get Doctor
        /// </summary>
        /// <returns>List of Doctor</returns>
        public async Task<List<Doctor>> GetAsync()
        {
            var doctors = _context.Doctors.ToList();
            return doctors;
        }

        /// <summary>
        /// Method to update doctor
        /// </summary>
        /// <param name="item">Object of doctor</param>
        /// <returns>DOctor object</returns>
        public async Task<Doctor> Update(Doctor item)
        {
            var doctor=await GetAsync(item.Id);
            _context.Entry<Doctor>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Doctor updated " + item.Id);
            return doctor;
        }
    }
}
