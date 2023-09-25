using UrlShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddDbContext<UrlInfoDataContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString("UrlInfoDb"))
                );

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.MapControllers();
                app.Run();
            }
        }
    }
}
