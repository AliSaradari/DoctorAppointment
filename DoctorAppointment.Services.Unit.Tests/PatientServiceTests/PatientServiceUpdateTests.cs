using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Exceptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.PatientServiceTests
{
    public class PatientServiceUpdateTests
    {
        private readonly PatientService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public PatientServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = PatientServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Update_updates_patient_properly()
        {
            var patient = new PatientBuilder()
                .Build();
            _context.Save(patient);
            var dto = UpdatePatientDtoFactory.Create();

            await _sut.Update(patient.Id, dto);

            var actual = _readContext.Patients.First(_ => _.Id == patient.Id);
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.NationalCode.Should().Be(dto.NationalCode);
        }
        [Fact]
        public async Task Update_throw_exeption_when_patient_doesnt_exist()
        {
            var id = 25;
            var dto = UpdatePatientDtoFactory.Create();

            var actual = () => _sut.Update(id, dto);

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistException>();
        }
    }
}
