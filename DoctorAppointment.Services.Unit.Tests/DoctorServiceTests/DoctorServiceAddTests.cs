using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.DoctorServiceTests
{
    public class DoctorServiceAddTests
    {
        private readonly DoctorService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public DoctorServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = DoctorServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Add_adds_a_new_doctor_properly()
        {
            var dto = AddDoctorDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Doctors.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.Field.Should().Be(dto.Field);
            actual.NationalCode.Should().Be(dto.NationalCode);
        }

        [Fact]
        public async Task Add_throw_exeption_when_national_code_is_duplicated()
        {
            var nationz = "67767";
            var doctor = new DoctorBuilder()
                .WithNationalCode(nationz)
                .Build();
            var dto = AddDoctorDtoFactory.Create(nationz);
            _context.Save(doctor);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CannotAddDoctorWithDuplicatedNationalCodeException>();
        }
    }
}
