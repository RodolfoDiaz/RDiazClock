using RDiazClock.Core.Services;
using RDiazClock.Web.Hubs;
using RDiazClock.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IClockService, ClockService>();
builder.Services.AddHostedService<ClockHostedService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapHub<ClockHub>("/clockHub");

app.Run();