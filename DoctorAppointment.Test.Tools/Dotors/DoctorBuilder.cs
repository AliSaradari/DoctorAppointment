using DoctorAppointment.Entities.Doctors;

namespace DoctorAppointment.Test.Tools.Dotors
{
    public  class DoctorBuilder
    {
        private readonly Doctor _doctor;
        public DoctorBuilder()
        {
            _doctor = new Doctor()
            {
                FirstName = "dummy-first-name",
                LastName = "dummy-last-name",
                Field = "heart",
                NationalCode = "dummy-national-code"
            };
        }
        public DoctorBuilder WithFirstName(string firstName)
        {
            _doctor.FirstName = firstName;
            return this;
        } 
        public DoctorBuilder WithLastName(string lastName)
        {
            _doctor.LastName = lastName;
            return this;
        } 
        public DoctorBuilder WithNationalCode(string nationalcode)
        {
            _doctor.NationalCode = nationalcode;
            return this;
        }
        public Doctor Build()
        {
            return _doctor;
        }
    }
}
