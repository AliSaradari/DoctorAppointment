using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dtos;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientRepository
    {
        void Add(Patient patient);
        void Delete(int id);
        Task<Patient?> FindById(int id);
        List<GetPatientDto> GetAll();
        bool IsExistNationalCode(string nationalCode);
    }
}
