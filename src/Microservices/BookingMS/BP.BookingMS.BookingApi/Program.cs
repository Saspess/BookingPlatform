using BP.Api.Common.Constants;
using BP.Api.Common.Middleware;
using BP.BookingMS.Business.IoC;
using BP.BookingMS.Data.Init;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureBookingBusiness(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment(ApplicationEnvironments.Docker))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DbInitializer.InitializeDatabase(app.Services);

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
