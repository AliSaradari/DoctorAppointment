using DoctorAppointment.Services.Patients.Contracts.Dtos;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientService
    {
        Task Add(AddPatientDto dto);
        Task Update(int id, UpdatePatientDto dto);
        Task Delete(int id);
        List<GetPatientDto> GetAll();
    }
}
