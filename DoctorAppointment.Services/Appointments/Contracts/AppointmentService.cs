using DoctorAppointment.Services.Appointments.Contracts.Dtos;

namespace DoctorAppointment.Services.Appointments.Contracts
{
    public interface AppointmentService
    {
        Task Add(AddAppointmentDto dto);
        Task Delete(int id);
        Task<List<GetAppointmentDto>> GetAll();
        Task Update(int id, UpdateAppointmentDto dto);
    }
}
