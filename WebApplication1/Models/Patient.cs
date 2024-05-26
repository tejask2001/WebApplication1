namespace DoctorsApp.Models
{
    public class Patient:IEquatable<Patient>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Patient()
        {
            
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="id">ID in int</param>
        /// <param name="name">Name in string</param>
        /// <param name="age">Age in int</param>
        /// <param name="gender">Gender in string</param>
        /// <param name="mobileNumber">MobileNumber in string</param>
        public Patient(int id, string name, int age, string gender, string mobileNumber)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
            MobileNumber = mobileNumber;
        }

        /// <summary>
        /// Used to compare the Patient object
        /// </summary>
        /// <param name="other">Object of Patient</param>
        /// <returns>True if Patient Id is present else False</returns>
        public bool Equals(Patient? other)
        {
            var patient = other ?? new Patient();
            return this.Id.Equals(patient.Id);
        }
    }
}
