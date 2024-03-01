using DoctorAppointment.Persistance.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exeptions;
using DoctorAppointment.Test.Tools.Dotors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests;

public class DoctorServiceTests
{
    private readonly DoctorService _sut;
    private readonly EFDataContext _context;
    private readonly EFDataContext _readContext;

    public DoctorServiceTests()
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

        await actual.Should().ThrowExactlyAsync<CannotAddDoctorWithDuplicatedNationalCodeExeption>();
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


        await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistExeption>();
    }
    [Fact]
    public async Task GetAll_the_get_method_shows_the_count_properly()
    {

        var doctor = new DoctorBuilder().Build();
        var doctor2 = new DoctorBuilder().Build();
        var doctor3 = new DoctorBuilder().Build();
        _context.Save(doctor);
        _context.Save(doctor2);
        _context.Save(doctor3);
        var exepted = 3;

        var actual = _sut.GetAll();

        actual.Count.Should().Be(exepted);
    }
    [Fact]
    public async Task GetAll_the_get_method_shows_the_doctors_information_properly()
    {

        var doctor = new DoctorBuilder().Build();
        _context.Save(doctor);

        var actual = _sut.GetAll();

        var result = actual.Single();
        result.FirstName.Should().Be(doctor.FirstName);
        result.LastName.Should().Be(doctor.LastName);
        result.Field.Should().Be(doctor.Field);
        result.NationalCode.Should().Be(doctor.NationalCode);
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

        await actual.Should().ThrowExactlyAsync<DoctorWithThisIdDoesntExistExeption>();
    }
}
