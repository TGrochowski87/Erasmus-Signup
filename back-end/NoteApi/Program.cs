using Microsoft.EntityFrameworkCore;
using NoteApi.Service;
using NoteApi.DbModels;
using NoteApi.Repository;
using ErasmusRabbitContracts;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using FluentAssertions.Common;
using NoteApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<notedbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("NoteDb")));
builder.Services.AddRabbitMqServices();
builder.Services.AddTransient<INoteService, NoteService>();
builder.Services.AddTransient<INoteRepository, NoteRepository>();
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
}

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NoteAPI", Version = "v1" });
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped(sp => ActivatorUtilities.CreateInstance<PlanNoteVM>(sp));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoteApi v1"));
}

app.UseRouting();
app.UseSession();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();
app.UseCookiePolicy();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);

app.Run();

