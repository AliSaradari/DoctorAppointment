namespace DoctorAppointment.Services.Patients.Contracts.Dtos
{
    public class GetPatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}
