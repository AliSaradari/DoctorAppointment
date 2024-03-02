using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
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
    public class AppointmentServiceAddTests
    {
        private readonly AppointmentService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public AppointmentServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = AppointmentServiceFactory.Create(_context);
        }
        [Fact]
        public async void Add_add_a_new_appointment_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            var dto = AddAppointmentDtoFactory.Create(doctor.Id, patient.Id);

            await _sut.Add(dto);

            var actual = _readContext.Appointments.Single();
            actual.DoctorId.Should().Be(dto.DoctorId);
            actual.PatientId.Should().Be(dto.PatientId);
            actual.AppointmentDate.Should().Be(dto.AppointmentDate);
        }
        [Fact]
        public async void Add_throw_exeption_when_doctor_doesnt_exist()
        {
            var patient = new PatientBuilder().Build();
            var appointmentDate = DateTime.UtcNow;
            _context.Save(patient);
            var doctorId = 123;
            var dto = AddAppointmentDtoFactory.Create(doctorId, patient.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<DoctorNotFoundException>();
        }
        [Fact]
        public async void Add_throw_exeption_when_patient_doesnt_exist()
        {
            var doctor = new DoctorBuilder().Build();
            var appointmentDate = new DateTime(2024, 03, 02);
            _context.Save(doctor);
            var patientId = 523;
            var dto = AddAppointmentDtoFactory.Create(doctor.Id, patientId);
            
            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<PatientNotFoundExeption>();
        }

    }
}
