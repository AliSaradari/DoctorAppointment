using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.PatientServiceTests
{
    public class PatientServiceGetAllTests
    {
        private readonly PatientService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public PatientServiceGetAllTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = PatientServiceFactory.Create(_context);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_count_properly()
        {

            var patient = new PatientBuilder()
                .Build();
            var patient2 = new PatientBuilder()
                .Build();
            var patient3 = new PatientBuilder()
                .Build();
            _context.Save(patient);
            _context.Save(patient2);
            _context.Save(patient3);
            var exepted = 3;

            var actual = _sut.GetAll();

            actual.Count.Should().Be(exepted);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_patients_information_properly()
        {

            var patient = new PatientBuilder().Build();
            _context.Save(patient);

            var actual = _sut.GetAll();

            var result = actual.Single();
            result.FirstName.Should().Be(patient.FirstName);
            result.LastName.Should().Be(patient.LastName);
            result.NationalCode.Should().Be(patient.NationalCode);
        }
    }
}
