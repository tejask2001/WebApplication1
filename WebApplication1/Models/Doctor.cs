namespace DoctorsApp.Models
{
    public class Doctor:IEquatable<Doctor>
    {       
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Qualification { get; set; }
        public string? Experience { get; set; }
        public string? Specialty { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Doctor()
        {

        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="id">Id in int</param>
        /// <param name="name">Name in string</param>
        /// <param name="age">Age in int</param>
        /// <param name="qualification">Qualification in string</param>
        /// <param name="experience">Experience in string</param>
        /// <param name="specialty">Specialty in string</param>
        public Doctor(int id, string? name, int age, string? qualification, string? experience, string? specialty)
        {
            Id = id;
            Name = name;
            Age = age;
            Qualification = qualification;
            Experience = experience;
            Specialty = specialty;
        }

        /// <summary>
        /// Used to compare the Doctor object
        /// </summary>
        /// <param name="other">Object of Doctor</param>
        /// <returns>True if Doctor Id is present else False</returns>
        public bool Equals(Doctor? other)
        {
            var doctor = other ?? new Doctor();
            return this.Id.Equals(doctor.Id);
        }
    }
}
