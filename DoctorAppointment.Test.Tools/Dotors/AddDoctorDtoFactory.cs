using DoctorAppointment.Services.Doctors.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Dotors
{
    public static class AddDoctorDtoFactory
    {
        //public static AddDoctorDtoFactory Create()
        //{
        //    var _dto = new AddDoctorDto()
        //    {
        //        FirstName = "dummy-first-name2",
        //        LastName = "dummy-last-name2",
        //        Field = "heart2",
        //        NationalCode = "dummy-national-code"
        //    };

        //}
        public static AddDoctorDto Create(string? n = null)
        {
           var _dto = new AddDoctorDto()
            {
                FirstName = "dummy-first-name2",
                LastName = "dummy-last-name2",
                Field = "heart2",
                NationalCode = n ?? "789"
            };
            return _dto;
        }
    }
}
