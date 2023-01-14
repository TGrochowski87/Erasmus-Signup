using UserApi.Service;

using System.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using UserApi.Attributes;
using Microsoft.EntityFrameworkCore;
using UserApi.Repository;
using UserApi.DbModels;

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

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<UserdbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserAPI", Version = "v1" });
});
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthorizedService, AuthorizedService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession( options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly= true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserApi v1"));
}

app.UseRouting();
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.MapControllers();
app.UseCookiePolicy();
app.UseAuthorization();

app.Run();

