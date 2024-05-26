using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IAppointmentAdminService
    {
        public Task<List<Appointment>> GetAppointmentList();
        public Task<Appointment> AddAppointment(Appointment appointment);
        public Task<Appointment> UpdateAppointmentPatient(int appointmentId, int patientId);
        public Task<Appointment> UpdateAppointmentDoctor(int appointmentId, int doctorId);

        public Task<Appointment> DeleteAppointment(int id);
    }
}
