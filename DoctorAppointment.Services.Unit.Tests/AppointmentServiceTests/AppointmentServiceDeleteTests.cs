using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Exeptions;
using DoctorAppointment.Test.Tools.Appointments;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.AppointmentServiceTests
{
    public class AppointmentServiceDeleteTests
    {
        private readonly AppointmentService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public AppointmentServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = AppointmentServiceFactory.Create(_context);
        }
        [Fact]
        public async void Delete_delete_an_appointment_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            var appointment = new AppointmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .Build();
            _context.Save(appointment);
            var id = appointment.Id;

            _sut.Delete(id);

            var actual = _readContext.Appointments.FirstOrDefault();
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throw_exeption_when_appointment_doesnt_exist()
        {
            var id = 1234;

            var actual = () => _sut.Delete(id);

            await actual.Should().ThrowExactlyAsync<AppointmentNotFoundException>();
        }
    }

    
}
