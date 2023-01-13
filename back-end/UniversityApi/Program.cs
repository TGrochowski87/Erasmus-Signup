using Microsoft.EntityFrameworkCore;
using UniversityApi.DbModels;
using UniversityApi.Repository;
using UniversityApi.Service;
using ErasmusRabbitContracts;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:3001"
                          );
                      });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<UniversitydbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("university")));

builder.Services.AddRabbitMqServices();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityAPI", Version = "v1" });
});

builder.Services.AddTransient<IUniversityService, UniversityService>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();


var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityApi v1"));
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();


app.Run();
