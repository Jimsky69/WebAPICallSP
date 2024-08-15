namespace WebAPICallSP.Models.DB
{
    public class Input
    {
        public int ClinicId { get; set; }
        public string AppointmentDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PatientId { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
    }
}
