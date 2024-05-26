using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Models.DTOs;
using DoctorsApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientAdminService _adminService;
        IPatientUserService _userService;
        public PatientController(IPatientAdminService adminService, IPatientUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        /// <summary>
        /// Method to retrieve list of Patients using HttpGet
        /// </summary>
        /// <returns>List of Patients</returns>
        [HttpGet]
        public async Task<List<Patient>> GetPatient()
        {
            var patient=await _adminService.GetPatientList();
            return patient;
        }

        /// <summary>
        /// Method to retrieve Patients using HttpGet
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <returns>Patient object</returns>
        [Route("/GetPatientById")]
        [HttpGet]
        public async Task<Patient> GetPatientById(int id)
        {
            var patient = await _userService.GetPatient(id);
            return patient;
        }

        /// <summary>
        /// Method to add Patient using HttpPost
        /// </summary>
        /// <param name="patient">Object of patient</param>
        /// <returns>Patient object</returns>
        [HttpPost]
        public async Task<Patient> PostPatient(Patient patient)
        {
            patient=await _adminService.AddPatient(patient);
            return patient;
        }

        /// <summary>
        /// Method to update patient's age
        /// </summary>
        /// <param name="patientDTO">Object of PatientAgeDTO</param>
        /// <returns>Patient Object</returns>
        [HttpPut]
        public async Task<Patient> UpdatePatientAge(PatientAgeDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientAge(patientDTO.Id, patientDTO.Age);
            return patient;
        }

        /// <summary>
        /// Method to update patient's Mobile
        /// </summary>
        /// <param name="patientDTO">Object of PatientMobileDTO</param>
        /// <returns>Patient Object</returns>
        [Route("UpdateMobileNumber")]
        [HttpPut]
        public async Task<Patient> UpdatePatientMobile(PatientMobileDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientMobile(patientDTO.Id, patientDTO.Mobile);
            return patient;
        }

        [HttpDelete]
        public async Task<Patient> DeletePatient(int id)
        {
            var patient=await _adminService.DeletePatient(id);
            return patient;
        }
    }
}
