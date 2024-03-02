using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exeptions;

namespace DoctorAppointment.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private readonly PatientRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public PatientAppService(PatientRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddPatientDto dto)
        {
            var checkDuplicatedNationalCode = _repository.IsExistNationalCode(dto.NationalCode);
            if (checkDuplicatedNationalCode == true)
            {
                throw new CannotAddPatientWithDuplicatedNationalCodeException();
            }
            var patient = new Patient()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode,
            };
            _repository.Add(patient);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var patient = await _repository.FindById(id);
            if (patient == null)
            {
                throw new PatientWithThisIdDoesntExistException();
            }
            _repository.Delete(id);
            await _unitOfWork.Complete();
        }

        public List<GetPatientDto> GetAll()
        {
            return _repository.GetAll();
        }


        public async Task Update(int id, UpdatePatientDto dto)
        {
            var patient = await _repository.FindById(id);
            if (patient == null)
            {
                throw new PatientWithThisIdDoesntExistException();
            }
            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.NationalCode = dto.NationalCode;

            await _unitOfWork.Complete();


        }
    }
}
