using DoctorsApp.Models;

namespace DoctorsApp.Interfaces
{
    public interface IAppointmentUserService
    {
        public Task<Appointment> GetAppointment(int id);
    }
}
