using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.DoctorServiceTests
{
    public class DoctorServiceUpdateTests
    {
        private readonly DoctorService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public DoctorServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = DoctorServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Update_updates_doctor_properly()
        {
            var doctor = new DoctorBuilder().Build();
            _context.Save(doctor);
            var dto = UpdateDoctorDtoFactory.Create();

            await _sut.Update(doctor.Id, dto);

            var actual = _readContext.Doctors.First(_ => _.Id == doctor.Id);
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.Field.Should().Be(dto.Field);
            actual.NationalCode.Should().Be(dto.NationalCode);
        }
        [Fact]
        public async Task Update_throw_exeption_when_doctor_doesnt_exist()
        {
            var id = 12;
            var updateDto = UpdateDoctorDtoFactory.Create();

            var actual = () => _sut.Update(id, updateDto);


            await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistException>();
        }
    }
}
