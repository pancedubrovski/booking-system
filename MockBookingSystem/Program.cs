using MockBookingSystem.NewFolder;
using MockBookingSystem.Repositories;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
builder.Services.AddScoped<ISearchManager, SearchManager>();
builder.Services.AddScoped<ICheckStatusManager, CheckStatusManager>();
builder.Services.AddScoped<IBookingManager, BookingManager>();


builder.Services.AddScoped<IHotelRespository, HotelRepositiroy>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepositroy>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();


app.UseAuthorization();

app.MapControllers();

app.Run();
