
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Persistance.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dto;
using DoctorAppointment.Services.Doctors.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using System.Numerics;

namespace DoctorAppointment.Services.Unit.Tests;

public class DoctorServiceTests
{
    [Fact]
    public async Task Add_adds_a_new_doctor_properly()
    {
        //arrange
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));

        //act
        await sut.Add(dto);

        //assert
        var actual = readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Add_throw_exeption_when_national_code_is_duplicated()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        //arrange
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        var dto2 = new AddDoctorDto
        {
            FirstName = "dummy-first-name2",
            LastName = "dummy-last-name2",
            Field = "heart2",
            NationalCode = "dummy-national-code"
        };
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        await sut.Add(dto);
        //act
        var actual = () => sut.Add(dto2);
        //assert
        await actual.Should().ThrowExactlyAsync<CannotAddDoctorWithDuplicatedNationalCodeExeption>();
    }

    [Fact]
    public async Task Update_updates_doctor_properly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        //arrange
        var doctor = new Doctor
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        context.Save(doctor);
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "child",
            NationalCode = "updated-national-code"
        };

        //act
        await sut.Update(doctor.Id, updateDto);

        //assert
        var actual = readContext.Doctors.First(_ => _.Id == doctor.Id);
        actual.FirstName.Should().Be(updateDto.FirstName);
        actual.LastName.Should().Be(updateDto.LastName);
        actual.Field.Should().Be(updateDto.Field);
        actual.NationalCode.Should().Be(updateDto.NationalCode);
    }
    [Fact]
    public async Task Update_throw_exeption_when_doctor_doesnt_exist()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        //arrange
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var id = 12;
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "child",
            NationalCode = "updated-national-code"
        };
        //act
        var actual = () => sut.Update(id, updateDto);

        //assert
        await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistExeption>();
    }
    [Fact]
    public async Task GetAll_the_get_method_shows_the_count_properly()
    {
        //arrange
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        var dto2 = new AddDoctorDto
        {
            FirstName = "dummy-first-name2",
            LastName = "dummy-last-name2",
            Field = "heart2",
            NationalCode = "dummy-national-code2"
        };
        var dto3 = new AddDoctorDto
        {
            FirstName = "dummy-first-name3",
            LastName = "dummy-last-name3",
            Field = "heart3",
            NationalCode = "dummy-national-code3"
        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        await sut.Add(dto);
        await sut.Add(dto2);
        await sut.Add(dto3);
        var exepted = 3;
        //act
        sut.GetAll();
        //assert
        var actual = readContext.Doctors.ToList().Count;
        actual.Should().Be(exepted);       
    }
    [Fact]
    public async Task GetAll_the_get_method_shows_the_doctors_information_properly()
    {
        //arrange
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        await sut.Add(dto);
        //act
        sut.GetAll();
        //assert
        var actual = readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }
    [Fact]
    public async Task Delete_delete_a_doctor_properly()
    {
        
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "dummy-national-code"
        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        await sut.Add(dto);
        var Id = readContext.Doctors.Single().Id;

        sut.Delete(Id);

        var actual = readContext.Doctors.Any();
        actual.Should().BeFalse();
    }
    [Fact]
    public async Task Delete_throw_exeption_when_doctor_doesnt_exist()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var id = 12;
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "child",
            NationalCode = "national-code"
        };
        
        var actual = () => sut.Delete(id);

        await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistExeption>();
    }
}
