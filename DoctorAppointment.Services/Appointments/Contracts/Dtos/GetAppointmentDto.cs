namespace DoctorAppointment.Services.Appointments.Contracts.Dtos
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
