using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using DoctorAppointment.Services.Doctors.Exceptions;

namespace DoctorAppointment.Services.Doctors;

public class DoctorAppService : DoctorService
{
    private readonly DoctorRepository _repository;
    private readonly UnitOfWork _unitOfWork;

    public DoctorAppService(
        DoctorRepository repository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddDoctorDto dto)
    {        
        var checkDuplicatedNationalCode = _repository.IsExistNationalCode(dto.NationalCode);
        if (checkDuplicatedNationalCode == true)
        {
            throw new CannotAddDoctorWithDuplicatedNationalCodeException();
        }
        var doctor = new Doctor()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Field = dto.Field,
            NationalCode = dto.NationalCode,
            
        };

        _repository.Add(doctor);
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var doctor = await _repository.FindById(id);
        if (doctor == null)
        {
            throw new DoctorWithThisIdDoesntExistException();
        }
        _repository.Delete(id);
        await _unitOfWork.Complete();
    }

    public List<GetDoctorDto> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task Update(int id, UpdateDoctorDto dto)
    {
        var doctor = await _repository.FindById(id);
        if(doctor == null)
        {
            throw new DoctorWithThisIdDoesntExistException();
        }
        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Field = dto.Field;
        doctor.NationalCode = dto.NationalCode;

        await _unitOfWork.Complete();
    }
}