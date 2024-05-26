namespace DoctorsApp.Models.DTOs
{
    public class RegisterDoctorDTO
    {
        public string? Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Qualification { get; set; }
        public string? Experience { get; set; }
        public string? Specialty { get; set; }
    }
}
