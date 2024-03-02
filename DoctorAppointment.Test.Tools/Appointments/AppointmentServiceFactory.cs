using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Persistance.EF.Appointments;
using DoctorAppointment.Services.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;

namespace DoctorAppointment.Test.Tools.Appointments
{
    public static class AppointmentServiceFactory
    {
        public static AppointmentService Create(EFDataContext context)
        {
            var repository = new EfAppointmentRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new AppointmentAppService(repository,unitOfWork);
        }
    }
}
