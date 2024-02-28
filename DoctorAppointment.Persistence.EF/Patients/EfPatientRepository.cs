using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistance.EF.Patients
{
    public class EfPatientRepository : PatientRepository
    {
        private EFDataContext _context;

        public EfPatientRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Patient patient)
        {
            _context.Add(patient);
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Patients.FirstOrDefault(p => p.Id == id));
        }

        public async Task<Patient?> FindById(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public List<GetPatientDto> GetAll()
        {
            return _context.Patients.Select(p => new GetPatientDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                NationalCode = p.NationalCode,
            }).ToList();
        }

        public bool IsExistNationalCode(string nationalCode)
        {
            return _context.Patients.Any(_ => _.NationalCode == nationalCode);
        }
    }
}
