using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniversityApi.DbModels;
using UniversityApi.Repository;
using UniversityApi.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<UniversityDataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("UniversityDb")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetEntryAssembly());
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
        cfg.UseMessageRetry(retryConfigurator =>
        {
            retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
        });
    });
});

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
