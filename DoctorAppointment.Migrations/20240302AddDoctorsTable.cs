using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Migrations
{
    [Migration(202403020336)]
    public class _202403020336octorsTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Doctors");
        }

        public override void Up()
        {
            Create.Table("Doctors")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("NationalCode").AsString(50).NotNullable()
                .WithColumn("Field").AsString(50).NotNullable();

        }
    }
}
