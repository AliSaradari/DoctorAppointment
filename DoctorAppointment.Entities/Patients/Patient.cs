using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Persons;

namespace DoctorAppointment.Entities.Patients
{
    public class Patient : Person
    {
        public List<Appointment> Appointments { get; set; }
    }
}
