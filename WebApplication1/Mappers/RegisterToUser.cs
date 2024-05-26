using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace DoctorsApp.Mappers
{
    public class RegisterToUser
    {
        User user;
        public RegisterToUser(RegisterDoctorDTO register)
        {
            user=new User();
            user.Username=register.Username;
            user.Role=register.Role;
            GetPassword(register.Password);
        }
        public RegisterToUser(RegisterPatientDTO register)
        {
            user = new User();
            user.Username = register.Username;
            user.Role = register.Role;
            GetPassword(register.Password);
        }

        void GetPassword(string password)
        {
            HMACSHA256 hmac=new HMACSHA256();
            user.Key = hmac.Key;
            user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public User GetUser()
        {
            return user;
        }
    }
}
