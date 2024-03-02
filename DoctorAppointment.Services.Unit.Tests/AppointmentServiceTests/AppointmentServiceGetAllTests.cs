using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Test.Tools.Appointments;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.AppointmentServiceTests
{
    public class AppointmentServiceGetAllTests
    {
        private readonly AppointmentService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public AppointmentServiceGetAllTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = AppointmentServiceFactory.Create(_context);
        }
        [Fact]
        public async void GetAll_the_get_method_shows_the_count_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            var appointment = new AppointmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .Build();
            var appointment2 = new AppointmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .Build();
            var appointment3 = new AppointmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .Build();
            _context.Save(appointment);
            _context.Save(appointment2);
            _context.Save(appointment3);
            var excepted = 3;

            var actual = await _sut.GetAll();

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_doctors_information_properly()
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

            var actual = await _sut.GetAll();

            var result = actual.Single();
            result.DoctorId.Should().Be(appointment.Id);
            result.PatientId.Should().Be(appointment.Id);
            result.AppointmentDate.Should().Be(appointment.AppointmentDate);
        }
    }
}