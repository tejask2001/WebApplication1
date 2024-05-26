using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Mappers;
using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace DoctorsApp.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositories<int, Doctor> _doctorRepository;
        private readonly IRepositories<int, Patient> _patientRepository;
        private readonly IRepositories<string, User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepositories<int, Doctor> doctorRepository,
            IRepositories<int, Patient> patientRepository,
            IRepositories<string, User> userRepository,
            ITokenService tokenService,
            ILogger<UserService> logger)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            var myUser = await _userRepository.GetAsync(user.Username);
            if (myUser == null)
            {
                throw new InvalidUserException();
            }
            var userPassword = GetPasswordEncrypted(user.Password, myUser.Key);
            var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
            if (checkPasswordMatch)
            {
                user.Password = "";
                user.Role = myUser.Role;
                user.Token = await _tokenService.GenerateToken(user);
                return user;
            }
            throw new InvalidUserException();
        }

        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                { return false; }
            }
            return true;
        }

        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA256 hmac = new HMACSHA256(key);
            var userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userPassword;
        }

        public async Task<LoginUserDTO> Register(RegisterDoctorDTO user)
        {
            var myUser = new RegisterToUser(user).GetUser();
            myUser = await _userRepository.Add(myUser);

            Doctor doctor = new RegisterToDoctor(user).GetDoctor();
            doctor = await _doctorRepository.Add(doctor);
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myUser.Username,
                Role = myUser.Role,
            };
            return result;
            throw new InvalidUserException();
        }

        public async Task<LoginUserDTO> Register(RegisterPatientDTO user)
        {
            var myUser = new RegisterToUser(user).GetUser();
            myUser = await _userRepository.Add(myUser);
            Patient patient = new RegisterToPatient(user).GetPatient();
            patient = await _patientRepository.Add(patient);
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myUser.Username,
                Role = myUser.Role,
            };
            return result;
        }
    }
}
