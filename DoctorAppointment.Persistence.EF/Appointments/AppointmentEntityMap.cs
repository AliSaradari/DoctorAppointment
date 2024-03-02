using DoctorAppointment.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointment.Persistance.EF.Appointments
{
    public class AppointmentEntityMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> _)
        {
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.DoctorId).IsRequired();
            _.Property(_ => _.PatientId).IsRequired();
            _.Property(_ => _.AppointmentDate).IsRequired();

            _.HasOne(_ => _.Doctor)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
            _.HasOne(_=> _.Patient)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
