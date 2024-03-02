using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.DoctorServiceTests
{
    public class DoctorServiceGetAllTests
    {
        private readonly DoctorService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public DoctorServiceGetAllTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = DoctorServiceFactory.Create(_context);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_count_properly()
        {

            var doctor = new DoctorBuilder().Build();
            var doctor2 = new DoctorBuilder().Build();
            var doctor3 = new DoctorBuilder().Build();
            _context.Save(doctor);
            _context.Save(doctor2);
            _context.Save(doctor3);
            var excepted = 3;

            var actual = _sut.GetAll();

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_doctors_information_properly()
        {

            var doctor = new DoctorBuilder().Build();
            _context.Save(doctor);

            var actual = _sut.GetAll();

            var result = actual.Single();
            result.FirstName.Should().Be(doctor.FirstName);
            result.LastName.Should().Be(doctor.LastName);
            result.Field.Should().Be(doctor.Field);
            result.NationalCode.Should().Be(doctor.NationalCode);
        }
    }
}
