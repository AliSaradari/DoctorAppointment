using DoctorAppointment.Services.Appointments.Contracts.Dtos;

namespace DoctorAppointment.Test.Tools.Appointments
{
    public static class AddAppointmentDtoFactory
    {
        public static AddAppointmentDto Create(int? doctorId = null, int? patientId = null)
        {
            return new AddAppointmentDto()
            {
                DoctorId = doctorId ?? 123,
                PatientId = patientId ?? 123,
                AppointmentDate = new DateTime(2024,03,02)
            };
        }
    }
}
