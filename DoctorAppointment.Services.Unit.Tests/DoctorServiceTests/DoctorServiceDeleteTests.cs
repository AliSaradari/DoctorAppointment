using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.DoctorServiceTests
{
    public class DoctorServiceDeleteTests
    {
        private readonly DoctorService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public DoctorServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = DoctorServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Delete_delete_a_doctor_properly()
        {

            var doctor = new DoctorBuilder().Build();
            _context.Save(doctor);
            var Id = doctor.Id;

            _sut.Delete(Id);

            var actual = _readContext.Doctors.Any();
            actual.Should().BeFalse();
        }
        [Fact]
        public async Task Delete_throw_exeption_when_doctor_doesnt_exist()
        {
            var id = 12;

            var actual = () => _sut.Delete(id);

            await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistException>();
        }
    }
}
