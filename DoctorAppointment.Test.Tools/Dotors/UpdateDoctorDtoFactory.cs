using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Dotors
{
    public static class UpdateDoctorDtoFactory
    {
        public static UpdateDoctorDto Create()
        {
            return new UpdateDoctorDto()
            {
                FirstName = "updated-dummy-first-name",
                LastName = "updated-dummy-last-name",
                Field = "child",
                NationalCode = "updated-national-code"
            };
        }
    }
}
