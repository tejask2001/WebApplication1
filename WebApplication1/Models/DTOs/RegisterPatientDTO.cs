namespace DoctorsApp.Models.DTOs
{
    public class RegisterPatientDTO
    {
        public string? Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
    }
}
