using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;
using DoctorsApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorAdminService _adminService;
        private readonly IDoctorUserService _userService;
        public DoctorController(IDoctorAdminService adminService, IDoctorUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        /// <summary>
        /// Method to retrieve list of Doctors using HttpGet
        /// </summary>
        /// <returns>List of Doctors</returns>
        [HttpGet]
        public async Task<List<Doctor>> GetDoctor()
        {
            var doctor = await _adminService.GetDoctorList();
            return doctor;
        }

        [Route("/GetById")]
        [HttpGet]
        [Authorize(Roles ="doctor")]
        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctor=await _userService.GetDoctor(id);
            return doctor;
        }
        /// <summary>
        /// Method to add list of Doctor using HttpPost
        /// </summary>
        /// <param name="doctor">object of Doctor</param>
        /// <returns>Doctor object</returns>
        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<Doctor> PostDoctor(Doctor doctor)
        {
            doctor = await _adminService.AddDoctor(doctor);
            return doctor;
        }

        /// <summary>
        /// Method to update doctor's experience
        /// </summary>
        /// <param name="doctorDTO">Object of DoctorExperienceDTO</param>
        /// <returns>object of Doctor</returns>
        [HttpPut]
        [Authorize(Roles = "doctor")]
        public async Task<Doctor> UpdateDoctorExperience(DoctorExperienceDTO doctorDTO)
        {
            var doctor = await _adminService.UpdateDoctorExperience(doctorDTO.Id, doctorDTO.Experience);
            return doctor;
        }

        /// <summary>
        /// Method to update doctor's qualification
        /// </summary>
        /// <param name="doctorDTO">Object of DoctorQualificationDTO</param>
        /// <returns>object of Doctor</returns>
        [Route("/UpdateQualification")]
        [HttpPut]
        [Authorize(Roles = "doctor")]
        public async Task<Doctor> UpdateDoctorQualification(DoctorQualificationDTO doctorDTO)
        {
            var doctor = await _adminService.UpdateDoctorQualification(doctorDTO.Id, doctorDTO.Qualification);
            return doctor;
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<Doctor> DeleteDoctor(int id)
        {
            var doctor=await _adminService.DeleteDoctor(id);
            return doctor;
        }
    }
}
