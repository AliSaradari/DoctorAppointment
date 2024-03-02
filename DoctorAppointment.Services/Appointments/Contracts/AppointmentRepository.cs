using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;

namespace DoctorAppointment.Services.Appointments.Contracts
{
    public interface AppointmentRepository
    {
        void Add(Appointment appointment);
        void Delete(int id);
        Appointment FindById(int id);
        List<GetAppointmentDto> GetAll();
        bool IsExistDoctor(int id);
        bool IsExistPatient(int id);
    }
}
