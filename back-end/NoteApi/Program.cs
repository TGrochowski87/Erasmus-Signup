using Microsoft.EntityFrameworkCore;
using NoteApi.Service;
using NoteApi.DbModels;
using NoteApi.Repository;
using Communication;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using UserApi.Service;
using FluentAssertions.Common;
using NoteApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
                            "http://localhost:3000",
                            "http://localhost:3001"
                          );
                      });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPlayground", Version = "v1" });
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = HeaderNames.Authorization,
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
     });
});
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IAuthorizedService, AuthorizedService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped(sp => ActivatorUtilities.CreateInstance<PlanNoteVM>(sp));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

    //options.Cookie.SameSite = SameSiteMode.Strict;
    //options.Cookie.Domain = "localhost::"; //using https://localhost:44340/ here doesn't work
    //options.Cookie.Expiration = DateTime.Now() + DateTime.UtcNow.AddDays(14);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseSession();

app.UseHttpsRedirection();
app.MapControllers();
app.UseCookiePolicy();
app.UseAuthorization();

app.Run();

