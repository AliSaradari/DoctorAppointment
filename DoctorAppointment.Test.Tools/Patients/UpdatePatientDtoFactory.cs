using DoctorAppointment.Services.Patients.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Patients
{
    public static class UpdatePatientDtoFactory
    {
        public static UpdatePatientDto Create()
        {
            return new UpdatePatientDto()
            {
                FirstName = "updated-dummy-first-name",
                LastName = "updated-dummy-last-name",
                NationalCode = "updated-national-code"
            };
        }
    }
}
