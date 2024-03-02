using DoctorAppointment.Entities.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointment.Persistance.EF.Doctors;

public class DoctorEntityMap : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        builder.Property(_ => _.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(_ => _.LastName).IsRequired().HasMaxLength(50);
        builder.Property(_ => _.Field).IsRequired().HasMaxLength(50);
        builder.Property(_ => _.NationalCode).IsRequired().HasMaxLength(50);
    }
}