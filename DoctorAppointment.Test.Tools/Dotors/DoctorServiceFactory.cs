using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Persistance.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;

namespace DoctorAppointment.Test.Tools.Dotors
{
    public static class DoctorServiceFactory
    {
        public static DoctorService Create(EFDataContext context)
        {
            var repository = new EFDoctorRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new DoctorAppService(repository, unitOfWork);
        }
    }
}
