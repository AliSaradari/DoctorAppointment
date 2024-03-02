using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;

namespace DoctorAppointment.Entities.Appointments
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
