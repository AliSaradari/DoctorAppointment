using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistance.EF.Doctors;

public class EFDoctorRepository : DoctorRepository
{
    private readonly EFDataContext _context;

    public EFDoctorRepository(EFDataContext context)
    {
        _context = context;
    }

    public void Add(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
    }

    public void Delete(int id)
    {      
        _context.Remove(_context.Doctors.FirstOrDefault(_ => _.Id == id));
    }

    public async Task<Doctor?> FindById(int id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public List<GetDoctorDto> GetAll()
    {
        return _context.Doctors.Select(d => new GetDoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Field = d.Field,
            NationalCode = d.NationalCode,
        }).ToList();
    }

    public bool IsExistNationalCode(string nationalCode)
    {
        return _context.Doctors.Any(_ => _.NationalCode == nationalCode);
    }

    public void Update(int id, Doctor doctor)
    {
        throw new NotImplementedException();
    }
}