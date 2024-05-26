using DoctorsApp.Context;
using DoctorsApp.Exceptions;
using DoctorsApp.Interfaces;
using DoctorsApp.Models;
using DoctorsApp.Repositories;
using DoctorsApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppTest
{
    internal class AppointmentRepositoryTest
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
        /// Method to test AddAppointment functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task AddAppointmentTest()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();
            IRepositories<int,Appointment> appointmentRepo= new AppointmentRepository(context, mockAppointmentRepoLogger.Object);
            IAppointmentAdminService admin = new AppointmentService(appointmentRepo);

            var mockDoctorRepoLogger = new Mock<ILogger<DoctorRepository>>();
            IRepositories<int, Doctor> doctorRepo = new DoctorRepository(context, mockDoctorRepoLogger.Object);
            IDoctorAdminService doctorAdmin = new DoctorService(doctorRepo);

            var mockPatientRepoLogger = new Mock<ILogger<PatientRepository>>();
            IRepositories<int, Patient> patientRepo = new PatientRepository(context, mockPatientRepoLogger.Object);
            IPatientAdminService patientAdmin = new PatientService(patientRepo);

            Doctor doctor = new Doctor()
            {
                Name = "Ninad",
                Age = 45,
                Qualification = "MBBS",
                Experience = "15",
                Specialty = "Ortho"
            };

            var doctorTest = await doctorAdmin.AddDoctor(doctor);

            Doctor doctor1 = new Doctor()
            {
                Name = "Natkar",
                Age = 54,
                Qualification = "MD",
                Experience = "17",
                Specialty = "Cardio"
            };
            var doctorTest1 = await doctorAdmin.AddDoctor(doctor1);

            Patient patient = new Patient()
            {
                Name = "Urjit",
                Age = 21,
                Gender = "male",
                MobileNumber = "9112317177"
            };

            var patients = await patientAdmin.AddPatient(patient);

            Patient patient1 = new Patient()
            {
                Name = "Harshal",
                Age = 20,
                Gender = "male",
                MobileNumber = "9764128858"
            };

            var patients1 = await patientAdmin.AddPatient(patient1);

            Appointment appointment = new Appointment()
            {
                DoctorId = 1,
                PatientId = 1,
            };

            var addAppointment= await admin.AddAppointment(appointment);

            Assert.AreEqual(addAppointment.DoctorId, doctor.Id);
        }

        /// <summary>
        /// Method to test GetAppointmentList functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllAppointment()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();
            IRepositories<int, Appointment> appointmentRepo = new AppointmentRepository(context, mockAppointmentRepoLogger.Object);
            IAppointmentAdminService appointmentAdmin = new AppointmentService(appointmentRepo);

            var appointmentList = await appointmentAdmin.GetAppointmentList();
            Assert.IsNotNull(appointmentList);
        }

        /// <summary>
        /// Method to test GetAppointment functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAppointmentById()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();
            IRepositories<int, Appointment> appointmentRepo = new AppointmentRepository(context, mockAppointmentRepoLogger.Object);
            IAppointmentAdminService admin = new AppointmentService(appointmentRepo);
            IAppointmentUserService user = new AppointmentService(appointmentRepo);

            var appointmentById = await user.GetAppointment(1);
            Assert.AreEqual(appointmentById.Doctor.Name,"Ninad");
        }

        /// <summary>
        /// Method to test UpdateAppointmentDoctor functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateAppointmentDoctor()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();
            IRepositories<int,Appointment> appointmentRepo= new AppointmentRepository(context,mockAppointmentRepoLogger.Object);
            IAppointmentAdminService admin=new AppointmentService(appointmentRepo);

            Appointment appointment = new Appointment();
            appointment.DoctorId = 2;

            var updatedAppointment = await admin.UpdateAppointmentDoctor(1, appointment.DoctorId);
            Assert.AreEqual(updatedAppointment.DoctorId,appointment.DoctorId);

        }

        /// <summary>
        /// Method to test UpdateAppointmentPatient functionality.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateAppointmentPatient()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();
            IRepositories<int, Appointment> appointmentRepo = new AppointmentRepository(context, mockAppointmentRepoLogger.Object);
            IAppointmentAdminService admin = new AppointmentService(appointmentRepo);

            Appointment appointment = new Appointment();
            appointment.PatientId = 1;

            var updatedAppointment = await admin.UpdateAppointmentPatient(1, appointment.PatientId);
            Assert.AreEqual(updatedAppointment.PatientId, appointment.PatientId);

        }

        /// <summary>
        /// Method to test NoSuchPatientException
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AppointmentException()
        {
            var mockAppointmentRepoLogger = new Mock<ILogger<AppointmentRepository>>();

            IRepositories<int, Appointment> appointmentRepo = new AppointmentRepository(context, mockAppointmentRepoLogger.Object);
            IAppointmentUserService user = new AppointmentService(appointmentRepo);

            Assert.ThrowsAsync<NoSuchAppointmentException>(async () => await user.GetAppointment(4));
        }

    }
}
