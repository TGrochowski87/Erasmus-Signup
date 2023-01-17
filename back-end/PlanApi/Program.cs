using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PlanApi.DbModels;
using PlanApi.Repository;
using PlanApi.Service;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(
                            "https://erasmussignup.azurewebsites.net"
                          );
                      });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<PlandbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("PlanDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlanAPI", Version = "v1" });
});

builder.Services.AddTransient<IPlanService, PlanService>();
builder.Services.AddTransient<IPlanRepository, PlanRepository>();
builder.Services.AddHttpClient<IPlanService, PlanService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://planapi23.azurewebsites.net");
    httpClient.Timeout = new TimeSpan(0, 2, 0);
});

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanApi v1"));
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();



app.Run();

