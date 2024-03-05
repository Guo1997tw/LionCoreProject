using prjLion.Repository.Helpers;
using prjLion.Repository.Implements;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Mapping;
using prjLion.Service.Implements;
using prjLion.Service.Interfaces;
using prjLion.Service.Mapping;
using prjLion.WebAPI.Mapping;

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
            builder.Services.AddScoped<ILionConnection, LionConnection>();
            builder.Services.AddScoped<ILionGetRepositorys, LionGetRepositorys>();
            builder.Services.AddScoped<ILionGetServices, LionGetServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}