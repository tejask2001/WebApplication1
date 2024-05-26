namespace DoctorsApp.Exceptions
{
    public class NoSuchPatientException:Exception
    {
        private string _message;
        public NoSuchPatientException()
        {
            _message = "No Patient found with given Id";
        }
        public override string Message => _message;
    }
}
