using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Persistance.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dtos;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistance.EF.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dtos;
using DoctorAppointment.Services.Doctors.Exeptions;
using DoctorAppointment.Services.Doctors;

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
            var patient = new Patient()
            {
                FirstName = "dummy_first_name",
                LastName = "dummy_first_name",
                NationalCode = "dummy_national_code"
            };
            var dto = new AddPatientDto()
            {
                FirstName = "dummy_first_name2",
                LastName = "dummy_first_name2",
                NationalCode = "dummy_national_code"
            };
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));            
            context.Save(patient);

            var actual = () => sut.Add(dto);

            actual.Should().ThrowExactlyAsync<CannotAddPatientWithDuplicatedNationalCodeExeption>();
        }

        [Fact]
        public async Task Update_updates_patient_properly()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var patient = new Patient
            {
                FirstName = "dummy-first-name",
                LastName = "dummy-last-name",                
                NationalCode = "dummy-national-code"
            };
            context.Save(patient);
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            var dto = new UpdatePatientDto
            {
                FirstName = "updated-dummy-first-name",
                LastName = "updated-dummy-last-name",                
                NationalCode = "updated-national-code"
            };
           
            await sut.Update(patient.Id, dto);
            
            var actual = readContext.Patients.First(_ => _.Id == patient.Id);
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);            
            actual.NationalCode.Should().Be(dto.NationalCode);
        }
        [Fact]
        public async Task Update_throw_exeption_when_patient_doesnt_exist()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            var id = 25;
            var dto = new UpdatePatientDto
            {
                FirstName = "updated-dummy-first-name",
                LastName = "updated-dummy-last-name",
                NationalCode = "updated-national-code"
            };

            var actual = () => sut.Update(id, dto);

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistExeption>();
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_count_properly()
        {

            var patient = new Patient
            {
                FirstName = "dummy-first-name",
                LastName = "dummy-last-name",
                NationalCode = "dummy-national-code"
            };
            var patient2 = new Patient
            {
                FirstName = "dummy-first-name2",
                LastName = "dummy-last-name2",
                NationalCode = "dummy-national-code2"
            };
            var patient3 = new Patient
            {
                FirstName = "dummy-first-name3",
                LastName = "dummy-last-name3",
                NationalCode = "dummy-national-code3"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            context.Save(patient);
            context.Save(patient2);
            context.Save(patient3);
            var exepted = 3;

            sut.GetAll();

            var actual = readContext.Patients.ToList().Count;
            actual.Should().Be(exepted);
        }
        [Fact]
        public async Task GetAll_the_get_method_shows_the_patients_information_properly()
        {

            var patient = new Patient
            {
                FirstName = "dummy-first-name",
                LastName = "dummy-last-name",
                NationalCode = "dummy-national-code"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            context.Save(patient);

            sut.GetAll();

            var actual = readContext.Patients.Single();
            actual.FirstName.Should().Be(patient.FirstName);
            actual.LastName.Should().Be(patient.LastName);
            actual.NationalCode.Should().Be(patient.NationalCode);
        }
        [Fact]
        public async Task Delete_delete_a_patient_properly()
        {

            var patient = new Patient
            {
                FirstName = "dummy-first-name",
                LastName = "dummy-last-name",
                NationalCode = "dummy-national-code"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            context.Save(patient);
            var Id = readContext.Patients.Single().Id;

            sut.Delete(Id);

            var actual = readContext.Patients.Any();
            actual.Should().BeFalse();
        }
        [Fact]
        public async Task Delete_throw_exeption_when_Patient_doesnt_exist()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EfPatientRepository(context), new EFUnitOfWork(context));
            var id = 42;           

            var actual = () => sut.Delete(id);

            await actual.Should().ThrowExactlyAsync<PatientWithThisIdDoesntExistExeption>();
        }
    }

    
}
