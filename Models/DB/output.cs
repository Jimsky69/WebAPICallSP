using System.ComponentModel.DataAnnotations;

namespace WebAPICallSP.Models.DB
{
    public partial class output
    {
        [Key]
        public int AppointmentId { get; set; }
        public int ReturnCode { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
}
