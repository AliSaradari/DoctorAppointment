using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using DoctorAppointment.Test.Tools.Patients;
using DoctorAppointment.Services.Patients.Contracts;

namespace DoctorAppointment.Services.Unit.Tests
{
    public class PatientServiceTest
    {
        private readonly PatientService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public PatientServiceTest()
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

            actual.Should().ThrowExactlyAsync<CannotAddPatientWithDuplicatedNationalCodeExeption>();
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

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistExeption>();
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

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistExeption>();
        }
    }


}
