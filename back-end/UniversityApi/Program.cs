using Microsoft.EntityFrameworkCore;
using UniversityApi.DbModels;
using UniversityApi.Repository;
using UniversityApi.Service;
using Communication;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<UniversityDataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("UniversityDb")));

builder.Services.AddRabbitMqServices();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUniversityService, UniversityService>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
