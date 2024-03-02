using DoctorAppointment.Entities.Appointments;

namespace DoctorAppointment.Test.Tools.Appointments
{
    public class AppointmentBuilder
    {
        private readonly Appointment _appointment;
        public AppointmentBuilder()
        {
            //var db = new EFInMemoryDatabase();
            //var _context = db.CreateDataContext<EFDataContext>();
            //var doctor = new DoctorBuilder().Build();
            //var patient = new PatientBuilder().Build();
            //_context.Save(doctor);
            //_context.Save(patient);
            _appointment = new Appointment()
            {
                DoctorId = 111,
                PatientId = 111,
                AppointmentDate = new DateTime(2024, 03, 02)
            };
        }
        public AppointmentBuilder WithDoctorId(int id)
        {
            _appointment.DoctorId = id;
            return this;
        }
        public AppointmentBuilder WithPatientId(int id)
        {
            _appointment.PatientId = id;
            return this;
        }
        public AppointmentBuilder AppointmentDate(DateTime appointmentDate)
        {
            _appointment.AppointmentDate = appointmentDate;
            return this;
        }
        public Appointment Build()
        {
            return _appointment;
        }
    }
}
