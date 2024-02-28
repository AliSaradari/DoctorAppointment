using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool IsExistNationalCode(string nationalCode)
        {
            return _context.Patients.Any(_ => _.NationalCode == nationalCode);
        }
    }
}
