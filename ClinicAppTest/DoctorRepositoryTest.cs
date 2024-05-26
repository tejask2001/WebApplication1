using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Repositories;
using DoctorsApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClinicAppTest
{
    public class DoctorRepositoryTest
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
        /// Method to test AddDoctor functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task AddDoctor()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            IDoctorAdminService admin = new DoctorService(doctorRepo);
            

            Doctor doctor = new Doctor()
            {
                Name = "test",
                Age = 45,
                Qualification = "MBBS",
                Experience = "15",
                Specialty = "Ortho"
            };

            var doctorTest = await admin.AddDoctor(doctor);

            Assert.AreEqual(doctorTest.Name, doctor.Name);
        }

        /// <summary>
        /// Method to test GetDoctorList functionality 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllDoctors()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            IDoctorAdminService admin = new DoctorService(doctorRepo);

            var doctors = await admin.GetDoctorList();
            Assert.IsNotEmpty(doctors);
        }

        [Test]
        public async Task GetDoctorsById()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();
            

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            IDoctorUserService user = new DoctorService(doctorRepo);

            var doctors = await user.GetDoctor(1);
            Assert.AreEqual(doctors.Name,"Ninad");
        }

        /// <summary>
        /// Method to test UpdateDoctorExperience functionality
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateDoctorExperience()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            
            IDoctorAdminService admin = new DoctorService(doctorRepo);

            Doctor doctor = new Doctor();
            doctor.Qualification = "MD";
            var updateDoctor = await admin.UpdateDoctorQualification(1, doctor.Qualification);

            Assert.AreEqual(updateDoctor.Qualification, doctor.Qualification);
        }

        /// <summary>
        /// Method to test UpdateDoctorSpecialty functionality
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateDoctorSpecialty()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();
            var mockDoctorServiceLogger = new Mock<ILogger<DoctorService>>();

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);

            IDoctorAdminService admin = new DoctorService(doctorRepo);

            Doctor doctor = new Doctor();

            doctor.Experience = "18";
            var updateDoctor = await admin.UpdateDoctorExperience(1, doctor.Experience);

            Assert.AreEqual(updateDoctor.Experience,doctor.Experience);
        }

        /// <summary>
        /// Method to test NoSuchDoctorException
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DoctorException()
        {
            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();
            var mockDoctorServiceLogger = new Mock<ILogger<DoctorService>>();

            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            IDoctorUserService user = new DoctorService(doctorRepo);

            Assert.ThrowsAsync<NoSuchDoctorException>(async () => await user.GetDoctor(4));
        }
    }
}