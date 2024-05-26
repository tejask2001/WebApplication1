namespace DoctorsApp.Exceptions
{
    public class NoSuchDoctorException:Exception
    {
        private string _message;
        public NoSuchDoctorException()
        {
            _message = "No doctor found with given Id";
        }
        public override string Message => _message;
    }
}
