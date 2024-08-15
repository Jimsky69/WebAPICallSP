using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAPICallSP.Models.DB;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.IO;
using WebAPICallSP.Filters;
using Azure.Messaging;



namespace WebAPICallSP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class outputsController : ControllerBase
    {
       private readonly DB_Demo_APIContext _context;
       private readonly IConfiguration _config;
        
        public outputsController(DB_Demo_APIContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }
        
        [HttpGet]
        //[ApiKeyAuthGet]
        public async Task<ActionResult<IEnumerable<AppointmentList>>> GetAppointmentList()
        {    
            string connecTest = _config.GetValue<string>("ConnectionStrings:DevConnection");
            var output = await _context.AppointmentList.ToListAsync();
            
            if (output == null)
            {
                return NotFound();
            }

            return output;
        }

      
        [HttpPost]
        public async Task<ActionResult<IEnumerable<output>>> Insertoutput(Input input)
        {
            string StoredProc = "exec CreateAppointment " +
                    "@ClinicID = " + input.ClinicId + "," +
                    "@AppointmentDate = '" + input.AppointmentDate + "'," +
                    "@FirstName= '" + input.FirstName + "'," +
                    "@LastName= '" + input.LastName + "'," +
                    "@PatientID= " + input.PatientId + "," +
                    "@AppointmentStartTime= '" + input.AppointmentStartTime + "'," +
                    "@AppointmentEndTime= '" + input.AppointmentEndTime + "'";

            return await _context.output.FromSqlRaw(StoredProc).ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentList>> Getoutputbyid(int id)
        {
           
            var appointmentlistkey = await _context.AppointmentList.FindAsync(id);

            if (appointmentlistkey == null)
            {
                return NotFound();
            }

            return appointmentlistkey;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AppointmentList>> Deleteoutputbyid(int id)
        {
           
            var appointmentlistkey = _context.AppointmentList.FirstOrDefault(x => x.ClinicID == id);

            if (appointmentlistkey == null)
            {
                return NotFound();
            }

            _context.AppointmentList.Remove(appointmentlistkey);
            _context.SaveChanges();
            return NoContent(); 
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentList>>Updoutputbyid(int id, AppointmentList appointmentlist)
        {

            var appointmentlistkey = await _context.AppointmentList.FindAsync(id);

            if (appointmentlistkey == null)
            {
                return NotFound();
            }
            
            appointmentlistkey.AppointmentDate = appointmentlist.AppointmentDate;
            appointmentlistkey.FirstName = appointmentlist.FirstName;
            appointmentlistkey.LastName = appointmentlist.LastName;
            appointmentlistkey.PatientID = appointmentlist.PatientID;
            appointmentlistkey.AppointmentStartTime = appointmentlist.AppointmentStartTime;
            appointmentlistkey.AppointmentEndTime = appointmentlist.AppointmentEndTime;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!outputExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    
        private bool outputExists(int id)
        {
            return _context.output.Any(e => e.AppointmentId == id);
        }

        public class IndexModel : PageModel
        { 
            private readonly ILogger<IndexModel> _logger;

            public IndexModel(ILogger<IndexModel> logger)
            {
                _logger = logger;
            
            }
        
        }


    }
}
