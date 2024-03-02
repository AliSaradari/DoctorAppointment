using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Persons;

namespace DoctorAppointment.Entities.Doctors;

public class Doctor : Person
{
    public string Field { get; set; }
    public List<Appointment> Appointments { get; set; }
}
