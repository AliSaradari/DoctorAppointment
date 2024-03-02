using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Exeptions;

namespace DoctorAppointment.Services.Appointments
{
    public class AppointmentAppService : AppointmentService
    {
        private readonly AppointmentRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public AppointmentAppService(AppointmentRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Add(AddAppointmentDto dto)
        {
            var isExistDoctor = _repository.IsExistDoctor(dto.DoctorId);
            var isExistPatient = _repository.IsExistPatient(dto.PatientId);
            if (isExistDoctor == false)
            {
                throw new DoctorNotFoundException();
            }
            if (isExistPatient == false)
            {
                throw new PatientNotFoundExeption();
            }
            var appointment = new Appointment()
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
            };
            _repository.Add(appointment);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var appointment = _repository.FindById(id);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
            _repository.Delete(id);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetAppointmentDto>> GetAll()
        {
            var result = _repository.GetAll();
            return result;
        }

        public async Task Update(int id, UpdateAppointmentDto dto)
        {            
            var appointment = _repository.FindById(id);
            if(appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
            appointment.DoctorId = dto.DoctorId;
            appointment.PatientId = dto.PatientId;
            appointment.AppointmentDate = dto.AppointmentDate;
            await _unitOfWork.Complete();
        }

    }
}
