using DoctorAppointment.Services.Doctors.Contracts.Dto;

namespace DoctorAppointment.Services.Doctors.Contracts;

public interface DoctorService
{
    Task Add(AddDoctorDto dto);
    Task Update(int id,UpdateDoctorDto dto);
    Task Delete(int id);
    List<GetDoctorDto> GetAll();
    
}