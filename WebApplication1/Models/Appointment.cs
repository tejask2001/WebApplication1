using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorsApp.Models
{
    public class Appointment:IEquatable<Appointment>
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Appointment()
        {

        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <param name="doctorId">DoctorId in int</param>
        /// <param name="doctor">Optional parameter Doctor object</param>
        /// <param name="patientId">PatientId in int</param>
        /// <param name="patient">Optional parameter Doctor object</param>
        /// <param name="dateAndTime">Date and Time </param>
        public Appointment(int id, int doctorId, Doctor? doctor, int patientId, Patient? patient, DateTime dateAndTime)
        {
            Id = id;
            DoctorId = doctorId;
            Doctor = doctor;
            PatientId = patientId;
            Patient = patient;
            DateAndTime = dateAndTime;
        }

        /// <summary>
        /// Used to compare the Appointment object
        /// </summary>
        /// <param name="other">Object of Appointment</param>
        /// <returns>True if Appointment Id is present else False</returns>
        public bool Equals(Appointment? other)
        {
            var appointment = other ?? new Appointment();
            return this.Id.Equals(appointment.Id);
        }
    }
}
