using DoctorAppointment.Services.Patients.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Patients
{
    public static class AddPatientDtoFactory
    {
        public static AddPatientDto Create(string? nationalcode = null)
        {
            return new AddPatientDto()
            {
                FirstName = "dummy_first_name",
                LastName = "dummy_first_name",
                NationalCode = nationalcode ?? "dummy_national_code"
            };
        }
    }
}
