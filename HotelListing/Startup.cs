using AspNetCoreRateLimit;
using HotelListing.BLL.Configurations;
using HotelListing.BLL.DTO.Mail;
using HotelListing.BLL.Interfaces;
using HotelListing.BLL.Services;
using HotelListing.DAL.EF;
using HotelListing.DAL.Interfaces;
using HotelListing.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HotelListing.WEB;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
        );

        services.AddMemoryCache();

        services.ConfigureRateLimiting();
        services.AddHttpContextAccessor();
        services.AddInMemoryRateLimiting();
        services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

        services.ConfigureHttpCacheHeaders();
        services.AddAuthentication();
        services.ConfigureIdentity();
        services.ConfigureJWT(Configuration);

        services.AddCors(o =>
        {
            o.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddAutoMapper(typeof(MapperInitilizer));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICountryService, CountryService>();
        services.AddTransient<IHotelService, HotelService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IMailService, MailService>();
        services.AddScoped<IAuthManager, AuthManager>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddControllers(config =>
        {
            config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
            {
                Duration = 120
            });
        }).AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.ConfigureVersioning();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
        app.ConfigureExceptionHandler();
        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseResponseCaching();
        app.UseHttpCacheHeaders();
        app.UseIpRateLimiting();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}