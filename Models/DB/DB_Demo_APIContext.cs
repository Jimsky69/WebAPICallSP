using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPICallSP.Models.DB;

#nullable disable

namespace WebAPICallSP.Models.DB
{
    public partial class DB_Demo_APIContext : DbContext
    {
        
        public DB_Demo_APIContext()
        {
        }

        public DB_Demo_APIContext(DbContextOptions<DB_Demo_APIContext> options)
            : base(options)
        {
        }
        public DbSet<WebAPICallSP.Models.DB.output> output { get; set; } = default!;

        public DbSet<WebAPICallSP.Models.DB.AppointmentList> AppointmentList { get; set; } = default;

        //public DbSet<WebAPICallSP.Models.DB.AppointmentListKey> AppointmentListKey { get; set; } = default!;

        //public DbSet<WebAPICallSP.Models.DB.AppointmentListKey> AppointmentListKey { get; set; } = default!;
    }
}
