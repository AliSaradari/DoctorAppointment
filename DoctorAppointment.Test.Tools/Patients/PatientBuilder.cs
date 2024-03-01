using DoctorAppointment.Entities.Patients;

namespace DoctorAppointment.Test.Tools.Patients
{
    public class PatientBuilder
    {
        private readonly Patient _patient;
        public PatientBuilder()
        {
            _patient = new Patient()
            {
                FirstName = "dummy_first_name",
                LastName = "dummy_first_name",
                NationalCode = "dummy_national_code"
            };
        }
        public PatientBuilder WithFirstName(string firstName)
        {
            _patient.FirstName = firstName;
            return this;
        }
        public PatientBuilder WithLastName(string lastName)
        {
            _patient.LastName = lastName;
            return this;
        }
        public PatientBuilder WithNationalCode(string nationalCode)
        {
            _patient.NationalCode = nationalCode;
            return this;
        }
        public Patient Build()
        {
            return _patient;
        }
    }
}
