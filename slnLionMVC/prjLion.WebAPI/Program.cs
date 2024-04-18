using Hangfire;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using prjLion.Repository.Helpers;
using prjLion.Repository.Implements;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Mapping;
using prjLion.Service.Implements;
using prjLion.Service.Interfaces;
using prjLion.Service.Mapping;
using prjLion.WebAPI.Mapping;
using System.Reflection;

namespace prjLion.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Lion Pool API", Version = "v1" } );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                option.IncludeXmlComments(xmlPath);
            });

            // Add Options Pattern Connection String for DB
            builder.Services.Configure<ConnectionStringOptionsModel>(builder.Configuration.GetSection("LionOptions"));

            // Add AutoMapper DI
            builder.Services.AddAutoMapper(option =>
            {
                option.AddProfile<RepositoryProfile>();
                option.AddProfile<ServicesProfile>();
                option.AddProfile<PresentationProfile>();
            });

            // Add Function DI
            builder.Services.AddScoped<ILionConnection, LionConnection>();
            builder.Services.AddScoped<ILionGetRepositorys, LionGetRepositorys>();
            builder.Services.AddScoped<ILionPostRepositorys, LionPostRepositorys>();
            builder.Services.AddScoped<ILionGetServices, LionGetServices>();
            builder.Services.AddScoped<ILionPostServices, LionPostServices>();

            // Add CORS DI
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: "AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("https://localhost:7073").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            // Add Upload Picture Size
            builder.Services.Configure<KestrelServerOptions>(option =>
            {
                option.Limits.MaxRequestBodySize = 52428800;
            });

            // Add Hangfire DI
            builder.Services.AddHangfire(option =>
            option.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("LionHW")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Lion Pool API v1");
                });
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            // Use HangFire
            app.UseHangfireDashboard("/LionFire");

            app.MapControllers();

            app.Run();
        }
    }
}