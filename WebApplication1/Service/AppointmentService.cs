using DoctorsApp.Interfaces;
using DoctorsApp.Models;

namespace DoctorsApp.Service
{
    public class AppointmentService : IAppointmentAdminService, IAppointmentUserService
    {
        IRepositories<int, Appointment> _repo;
        public AppointmentService(IRepositories<int, Appointment> repo)
        {
            _repo = repo;
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            appointment=await _repo.Add(appointment);
            return appointment;
        }

        public async Task<Appointment> DeleteAppointment(int id)
        {
            var appointment = await _repo.Delete(id);
            return appointment;
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            var appointment= await _repo.GetAsync(id);
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentList()
        {
            var appointments = await _repo.GetAsync();
            return appointments;
        }

        public async Task<Appointment> UpdateAppointmentDoctor(int appointmentId, int doctorId)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null) 
            {
                appointment.DoctorId = doctorId;
                appointment=await _repo.Update(appointment);
                return appointment;

            }
            return null;
        }

        public async Task<Appointment> UpdateAppointmentPatient(int appointmentId, int patientId)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null)
            {
                appointment.PatientId = patientId;
                appointment = await _repo.Update(appointment);
                return appointment;

            }
            return null;
        }
    }
}
