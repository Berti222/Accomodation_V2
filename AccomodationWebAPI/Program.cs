using AccomodationModel.AccomodationRepository;
using AccomodationModel.Models;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Logic.Factories;
using AccomodationWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AccomodationContext>(
    options => options.UseSqlServer("ConnectionStrings:Accomodation"));

builder.Services.AddSingleton<ILoggingHelper, LoggingHelper>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IPagingFactroy, PagingFactroy>();
builder.Services.AddScoped<AllergenicLogic>();
builder.Services.AddScoped<RoomPriceLogic>();
builder.Services.AddScoped<RoomTypeLogic>();
builder.Services.AddScoped<EquipmentLogic>();
builder.Services.AddScoped<RoomLogic>();
builder.Services.AddScoped<GuestLogic>();

builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateBootstrapLogger();

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
