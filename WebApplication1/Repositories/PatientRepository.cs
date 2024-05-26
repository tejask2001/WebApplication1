using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoctorsApp.Repositories
{
    public class PatientRepository : IRepositories<int, Patient>
    {
        RequestTrackerContext _context;
        ILogger<PatientRepository> _logger;
        public PatientRepository(RequestTrackerContext context, ILogger<PatientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Method to add Patient
        /// </summary>
        /// <param name="item">Object of patient</param>
        /// <returns>Patient object</returns>
        public async Task<Patient> Add(Patient item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Patient added " + item.Id);
            return item;
        }

        /// <summary>
        /// Method to delete Patient
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Patient Object</returns>
        public async Task<Patient> Delete(int key)
        {
            var patient=await GetAsync(key);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            _logger.LogInformation("Patient deleted " + key);
            return patient;
        }

        /// <summary>
        /// Method to Get Patient by Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Petient object</returns>
        /// <exception cref="NoSuchPatientException">Throws when no patient found</exception>
        public async Task<Patient> GetAsync(int key)
        {
            var patients = await GetAsync();
            var patient = patients.SingleOrDefault(e=>e.Id == key);
            if (patient != null)
            {
                return patient;
            }
            throw new NoSuchPatientException();
        }

        /// <summary>
        /// Method to get Patients
        /// </summary>
        /// <returns>List of Patients</returns>
        public async Task<List<Patient>> GetAsync()
        {
            var patient = _context.Patients.ToList();
            return patient;
        }

        /// <summary>
        /// Method to update Patient
        /// </summary>
        /// <param name="item">Object of patient</param>
        /// <returns>Patient object</returns>
        public async Task<Patient> Update(Patient item)
        {
            var patient = await GetAsync(item.Id);
            _context.Entry<Patient>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Patient updated " + item.Id);
            return patient; 
        }
    }
}