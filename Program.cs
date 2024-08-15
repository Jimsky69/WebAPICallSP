using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebAPICallSP.Models.DB;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DB_Demo_APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection") ?? throw new InvalidOperationException("Connection string 'LibraryAPIContext' not found.")));

//

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddRazorPages();

//builder.Logging.AddEventLog(eventLogSettings =>
//{
//    eventLogSettings.SourceName = "WebAPICallSp";
//}
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
