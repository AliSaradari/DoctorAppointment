using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using DoctorAppointment.Services.Appointments.Exeptions;
using DoctorAppointment.Test.Tools.Appointments;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.AppointmentServiceTests
{
    public class AppointmentServiceUpdateTests
    {
        private readonly AppointmentService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public AppointmentServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = AppointmentServiceFactory.Create(_context);
        }
        [Fact]
        public async void Update_update_an_appointment_properly()
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
            var doctor2 = new DoctorBuilder().Build();
            var patient2 = new PatientBuilder().Build();
            _context.Save(doctor2);
            _context.Save(patient2);
            var dto = UpdateAppointmentDtoFactory.Create(doctor2.Id,patient2.Id);           
            var id = appointment.Id;

            await _sut.Update(id, dto);

            var actual = _readContext.Appointments.Single();
            actual.DoctorId.Should().Be(dto.DoctorId);
            actual.PatientId.Should().Be(dto.PatientId);
            actual.AppointmentDate.Should().Be(dto.AppointmentDate);

        }
        [Fact]
        public async Task Update_throw_exeption_when_appointment_doesnt_exist()
        {
            var id = 12344;
            var dto = UpdateAppointmentDtoFactory.Create();

            var actual = () => _sut.Update(id, dto);

            await actual.Should().ThrowExactlyAsync<AppointmentNotFoundException>();

        }
    }


}
