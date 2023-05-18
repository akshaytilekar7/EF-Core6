using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
// newly added // ingore cyclic refrance while deserlizing C# to json

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// newly added
builder.Services.AddDbContext<PublisherData.PubContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PubConnection"))
    .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
