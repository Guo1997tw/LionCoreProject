using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using prjLionMVC.Implements;
using prjLionMVC.Interfaces;
using prjLionMVC.LogExceptions;
using prjLionMVC.Models.Entity;

namespace prjLionMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add LionHW Connection String
            builder.Services.AddDbContext<LionHwContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("LionHW"));
            });

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Interface DI
            builder.Services.AddScoped<ILion, Lion>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserAuthentication, UserAuthentication>();

            // Authentication DI
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Lion/Login";
                option.LogoutPath = "/Lion/Login";
                option.AccessDeniedPath = "/Lion/Error";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Use Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Error write DB
            app.UseMiddleware<ErrorLogs>();

            // Use UseAuthentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}