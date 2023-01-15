using UserApi.Service;
using System.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using UserApi.Repository;
using UserApi.DbModels;


namespace UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

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

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAuthorizedService, AuthorizedService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession( options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly= true;
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
            //app.UseEndpoints();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.Run();
        }
    }
}