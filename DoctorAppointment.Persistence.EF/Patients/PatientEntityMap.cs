using DoctorAppointment.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistance.EF.Patients
{
    public class PatientEntityMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> _)
        {
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.FirstName).IsRequired();
            _.Property(_ => _.LastName).IsRequired();            
            _.Property(_ => _.NationalCode).IsRequired();
        }
    }
}
