using DoctorAppointment.Services.Appointments.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Appointments
{
    public static class UpdateAppointmentDtoFactory
    {
        public static UpdateAppointmentDto Create(int? doctorId = null, int? patientId = null)
        {
            return new UpdateAppointmentDto()
            {
                DoctorId = doctorId ?? 321,
                PatientId = patientId ?? 321,
                AppointmentDate = new DateTime(2026, 06, 04)
            };
        }
    }
}
