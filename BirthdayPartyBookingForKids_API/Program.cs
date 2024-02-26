using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Repositoties.IRepository;
using Repositoties.Repository;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntityType<Booking>().HasKey(booking => booking.BookingId);
modelBuilder.EntitySet<Booking>("Bookings");
modelBuilder.EntityType<Menu>().HasKey(menu => menu.FoodId);
modelBuilder.EntitySet<Menu>("Menus");

// Add services to the container.
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories and DAO
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingDAO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
