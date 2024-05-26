namespace DoctorsApp.Exceptions
{
    public class NoSuchAppointmentException:Exception
    {
        private string _message;
        public NoSuchAppointmentException()
        {
            _message = "No Appointment found with given Id";
        }
        public override string Message => _message;
    }
}
