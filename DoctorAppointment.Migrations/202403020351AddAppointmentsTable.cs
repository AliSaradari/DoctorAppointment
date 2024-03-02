using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Migrations
{
    [Migration(202403020351)]
    public class _202403020351AddAppointmentsTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Appointments");
        }

        public override void Up()
        {
            Create.Table("Appointments")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("DoctorId").AsInt32().NotNullable().ForeignKey("Fk_Doctors_Appointments", "Doctors", "Id")
                .WithColumn("PatientId").AsInt32().NotNullable().ForeignKey("Fk_Patients_Appointments", "Patients", "Id")
                .WithColumn("AppointmentDate").AsDateTime().NotNullable();



        }
    }
}
