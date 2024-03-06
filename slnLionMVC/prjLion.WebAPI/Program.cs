using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using prjLion.Repository.Helpers;
using prjLion.Repository.Implements;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Mapping;
using prjLion.Service.Implements;
using prjLion.Service.Interfaces;
using prjLion.Service.Mapping;
using prjLion.WebAPI.Mapping;
using System.Text;

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
            builder.Services.AddSwaggerGen();

            // Add AutoMapper DI
            builder.Services.AddAutoMapper(option =>
            {
                option.AddProfile<RepositoryProfile>();
                option.AddProfile<ServicesProfile>();
                option.AddProfile<PresentationProfile>();
            });

            // Add Function DI
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ILionConnection, LionConnection>();
            builder.Services.AddScoped<ILionGetRepositorys, LionGetRepositorys>();
            builder.Services.AddScoped<ILionPostRepositorys, LionPostRepositorys>();
			builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
			builder.Services.AddScoped<ILionGetServices, LionGetServices>();
            builder.Services.AddScoped<ILionPostServices, LionPostServices>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Add CORS DI
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: "AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("https://localhost:7073").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

			// Add Authentication DI
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
			{
				option.LoginPath = PathString.FromUriComponent(new Uri("https://localhost:7073/Lion/Login"));
				option.AccessDeniedPath = PathString.FromUriComponent(new Uri("https://localhost:7073/Lion/Error"));
			});

			// Add Authentication JWT DI
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS
            app.UseCors("AllowSpecificOrigin");

            // Use Routing
            app.UseRouting();

            // Use UseAuthentication
            app.UseAuthentication();

            app.UseAuthorization();

            // Use JWT
            app.UseEndpoints(option =>
            {
                option.MapControllers();
            });

            // app.MapControllers();

            app.Run();
        }
    }
}