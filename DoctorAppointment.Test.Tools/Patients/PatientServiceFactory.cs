using DoctorAppointment.Persistance.EF.Patients;
using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts;

namespace DoctorAppointment.Test.Tools.Patients
{
    public static class PatientServiceFactory
    {
        public static PatientService Create(EFDataContext context)
        {
            var repository = new EfPatientRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new PatientAppService(repository,unitOfWork);
        }
    }
}
