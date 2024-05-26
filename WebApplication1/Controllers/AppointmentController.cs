using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;
using DoctorsApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        IAppointmentAdminService _appointmentAdminService;
        IAppointmentUserService _appointmentUserService;
        public AppointmentController(IAppointmentAdminService appointmentAdminService, IAppointmentUserService appointmentUserService)
        {
            _appointmentAdminService = appointmentAdminService;
            _appointmentUserService = appointmentUserService;
        }

        /// <summary>
        /// Method to retrieve list of Appointments using HttpGet
        /// </summary>
        /// <returns>List of Patients</returns>
        [HttpGet]
        public async Task<List<Appointment>> GetAppointment()
        {
            var appointment = await _appointmentAdminService.GetAppointmentList();
            return appointment;
        }

        /// <summary>
        /// Method to retrieve Appointment using HttpGet
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <returns>Appointment object</returns>
        [Route("/GetAppointmentById")]
        [HttpGet]
        public async Task<Appointment> GetAppointmentById(int id)
        {
            var appointment = await _appointmentUserService.GetAppointment(id);
            return appointment;
        }

        /// <summary>
        /// Method to add Appointment using HttpPost
        /// </summary>
        /// <param name="appointment">Object of Appointment</param>
        /// <returns>Appointment object</returns>
        [HttpPost]
        public async Task<Appointment> PostAppointment(Appointment appointment)
        {
            appointment = await _appointmentAdminService.AddAppointment(appointment);
            return appointment;
        }

        /// <summary>
        /// Method to update Appointment doctor
        /// </summary>
        /// <param name="appointmentDTO">object of AppointmentDoctorDTO</param>
        /// <returns>Appointment object</returns>
        [HttpPut]
        public async Task<Appointment> UpdateAppointmentDoctor(AppointmentDoctorDTO appointmentDTO)
        {
            var appointment = await _appointmentAdminService.UpdateAppointmentDoctor(appointmentDTO.Id, appointmentDTO.DoctorId);
            return appointment;
        }

        /// <summary>
        /// Method to update Appointment Patient
        /// </summary>
        /// <param name="appointmentDTO">object of AppointmentPatientDTO</param>
        /// <returns>Appointment object</returns>
        [Route("/UpdateAppointmentPatient")]
        [HttpPut]
        public async Task<Appointment> UpdateAppointmentPatient(AppointmentPatientDTO appointmentDTO)
        {
            var appointment = await _appointmentAdminService.UpdateAppointmentPatient(appointmentDTO.Id, appointmentDTO.PatientId);
            return appointment;
        }

        /// <summary>
        /// Method to delete appointment
        /// </summary>
        /// <param name="id">id in int</param>
        /// <returns>Object of Appointment</returns>
        [HttpDelete]
        public async Task<Appointment> DeleteAppointment(int id)
        {
            var appointment = await _appointmentAdminService.DeleteAppointment(id);
            return appointment;
        }
    }
}
