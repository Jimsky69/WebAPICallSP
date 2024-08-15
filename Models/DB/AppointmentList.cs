using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAPICallSP.Models.DB
{
    //[Keyless]
    public class AppointmentList
    {
        [Key]
        public int ClinicID { get; set; } 

        public string AppointmentDate { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public int PatientID { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }

    }
}
