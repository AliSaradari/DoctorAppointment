using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Persistance.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests
{
    public class PatientServiceTest
    {
        [Fact]
        public async Task Add_add_new_patient_properly()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var dto = new AddPatientDto()
            {
                FirstName = "dummy_first_name",
                LastName = "dummy_first_name",
                NationalCode = "dummy_first_name"
            };
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));

            await sut.Add(dto);

            var actual = readContext.Patients.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.NationalCode.Should().Be(dto.NationalCode);            
        }

        [Fact]
        public async Task Add_throw_exeption_when_national_code_is_duplicated()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();            
            var dto = new AddPatientDto()
            {
                FirstName = "dummy_first_name",
                LastName = "dummy_first_name",
                NationalCode = "dummy_national_code"
            };
            var dto2 = new AddPatientDto()
            {
                FirstName = "dummy_first_name2",
                LastName = "dummy_first_name2",
                NationalCode = "dummy_national_code"
            };
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            await sut.Add(dto);
            
            var actual = () => sut.Add(dto2);

            actual.Should().ThrowExactlyAsync<CannotAddPatientWithDuplicatedNationalCodeExeption>();            
        }
    }


}
