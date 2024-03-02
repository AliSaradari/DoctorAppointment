namespace DoctorAppointment.Services.Appointments.Contracts.Dtos
{
    public class AddAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
