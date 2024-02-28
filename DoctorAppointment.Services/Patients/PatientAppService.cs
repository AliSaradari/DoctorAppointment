using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Doctors.Exeptions;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new CannotAddPatientWithDuplicatedNationalCodeExeption();
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
    }
}
