using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using OpinionApi.DbModels;
using OpinionApi.Repository;
using OpinionApi.Service;

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

builder.Services.AddDbContext<OpiniondbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("OpinionDb")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpinionAPI", Version = "v1" });
});

builder.Services.AddTransient<IOpinionService, OpinionService>();
builder.Services.AddTransient<IOpinionRepository, OpinionRepository>();


var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpinionApi v1"));
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

