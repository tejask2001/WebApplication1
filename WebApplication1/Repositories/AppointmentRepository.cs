



using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Service;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DoctorsApp.Repositories
{
    public class AppointmentRepository : IRepositories<int, Appointment>
    {
        RequestTrackerContext _context;
        private readonly ILogger<AppointmentRepository> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="context"></param>
        public AppointmentRepository(RequestTrackerContext context, ILogger<AppointmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Method to add Appointment
        /// </summary>
        /// <param name="item">Object of Appointment</param>
        /// <returns>Appointment object</returns>
        public async Task<Appointment> Add(Appointment item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Appointment added " + item.Id);
            return item;
        }

        public async Task<Appointment> Delete(int key)
        {
            var appointment=await GetAsync(key);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            _logger.LogInformation("Appointment deleted " + key);
            return appointment;
        }

        /// <summary>
        /// Method to Get appointment by Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Appointment object</returns>
        /// <exception cref="NoSuchAppointmentException">Throws when no appointment found</exception>
        public async Task<Appointment> GetAsync(int key)
        {
            var appointments = await GetAsync();
            var appointment= appointments.FirstOrDefault(e=>e.PatientId==key);
            if (appointment != null)
            {
                return appointment;
            }
            throw new NoSuchAppointmentException();
        }

        /// <summary>
        /// Method to get Appointment details
        /// </summary>
        /// <returns>List of appointments</returns>
        public async Task<List<Appointment>> GetAsync()
        {
            var appointment = _context.Appointments.Include(e => e.Patient).Include(d=>d.Doctor).ToList();
            return appointment;
        }

        /// <summary>
        /// Method to update Appointment
        /// </summary>
        /// <param name="item">Object of Appointment</param>
        /// <returns>Appointment object</returns>
        public async Task<Appointment> Update(Appointment item)
        {
            var appointment=await GetAsync(item.Id);
            _context.Entry<Appointment>(item).State=EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Appointment updated " + item.Id);
            return appointment;
        }
        
    }
}
