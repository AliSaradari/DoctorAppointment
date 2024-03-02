using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Exceptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.PatientServiceTests
{
    public class PatientServiceDeleteTests
    {
        private readonly PatientService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public PatientServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = PatientServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Delete_delete_a_patient_properly()
        {

            var patient = new PatientBuilder().Build();
            _context.Save(patient);
            var Id = patient.Id;

            _sut.Delete(Id);

            var actual = _readContext.Patients.Any();
            actual.Should().BeFalse();
        }
        [Fact]
        public async Task Delete_throw_exeption_when_Patient_doesnt_exist()
        {
            var id = 42;

            var actual = () => _sut.Delete(id);

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistException>();
        }
    }
}
