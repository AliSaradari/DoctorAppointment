using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;

namespace DoctorAppointment.Persistance.EF.Appointments
{
    public class EfAppointmentRepository : AppointmentRepository
    {
        private readonly EFDataContext _context;
        public EfAppointmentRepository(EFDataContext context)
        {
            _context = context;
        }
        public void Add(Appointment appointment)
        {
            _context.Add(appointment);
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Appointments.FirstOrDefault(_ => _.Id == id));
        }

        public Appointment FindById(int id)
        {
            return _context.Appointments.FirstOrDefault(_ => _.Id == id);
        }

        public List<GetAppointmentDto> GetAll()
        {
            return _context.Appointments.Select(a => new GetAppointmentDto()
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate
            }).ToList();
        }

        public bool IsExistDoctor(int id)
        {
            return _context.Doctors.Any(_ => _.Id == id);
        }

        public bool IsExistPatient(int id)
        {
            return _context.Patients.Any(_ => _.Id == id);
        }
    }
}
