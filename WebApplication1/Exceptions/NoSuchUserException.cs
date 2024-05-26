namespace DoctorsApp.Exceptions
{
    public class NoSuchUserException:Exception
    {
        private readonly string message;
        public NoSuchUserException()
        {
            message = "No user found with given details";
        }
        public override string Message => message;
    }
}
