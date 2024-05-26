using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Repositories;
using DoctorsApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppTest
{
    internal class PatientRepositoryTest
    {
        RequestTrackerContext context;

        /// <summary>
        /// Setup Method in which InMemory dummy database is created
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummyDatabase").Options;
            context = new RequestTrackerContext(options);
        }

        /// <summary>
        /// Method to test AddPatient functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task AddPatient()
        {
            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientAdminService admin = new PatientService(patientRepo);

            Patient patient = new Patient()
            {
                Name = "Urjit",
                Age = 21,
                Gender = "male",
                MobileNumber = "9112317177"
            };

            var patients = await admin.AddPatient(patient);
            Assert.AreEqual(patients.Name, patient.Name);

        }

        /// <summary>
        /// Method to test GetPatientList functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetPatients()
        {
            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientAdminService admin = new PatientService(patientRepo);

            var patients = await admin.GetPatientList();
            Assert.IsNotNull(patients);
        }

        /// <summary>
        /// Method to test GetPatient functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetPatientById()
        {
            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientUserService user = new PatientService(patientRepo);

            var patients = await user.GetPatient(1);
            Assert.AreEqual(patients.Name, "Urjit");
        }

        /// <summary>
        /// Method to test UpdatePatientAge functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdatePatientAge()
        {
            var mockPatientRepoLogger=new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientAdminService admin = new PatientService(patientRepo);

            Patient patient = new Patient();
            patient.Age = 24;

            var patients = await admin.UpdatePatientAge(1,patient.Age);
            Assert.AreEqual(patient.Age, patients.Age);
        }

        /// <summary>
        /// Method to test UpdatePatientMobile functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdatePatientMobile()
        {
            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientAdminService admin = new PatientService(patientRepo);

            Patient patient = new Patient();
            
            patient.MobileNumber = "9112317177";

            var patients = await admin.UpdatePatientMobile(1, patient.MobileNumber);
            Assert.AreEqual(patient.MobileNumber, patients.MobileNumber);
        }

        /// <summary>
        /// Method to test NoSuchPatientException
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task PatientException()
        {
            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();

            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientUserService user = new PatientService(patientRepo);

            Assert.ThrowsAsync<NoSuchPatientException>(async () => await user.GetPatient(4));
        }
    }
}