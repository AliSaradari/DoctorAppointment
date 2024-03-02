using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests.PatientServiceTests
{
    public class PatientServiceAddTests
    {
        private readonly PatientService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public PatientServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = PatientServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Add_add_new_patient_properly()
        {
            var dto = AddPatientDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Patients.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.NationalCode.Should().Be(dto.NationalCode);
        }

        [Fact]
        public async Task Add_throw_exeption_when_national_code_is_duplicated()
        {
            var nationalCode = "123";
            var patient = new PatientBuilder()
                .WithNationalCode(nationalCode)
                .Build();
            var dto = AddPatientDtoFactory.Create(nationalCode);
            _context.Save(patient);

            var actual = () => _sut.Add(dto);

            actual.Should().ThrowExactlyAsync<CannotAddPatientWithDuplicatedNationalCodeException>();
        }
    }
}
