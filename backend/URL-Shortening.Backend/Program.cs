// <copyright file="Program.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend
{
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.EntityFrameworkCore;
    using URL_Shortening_Service.Backend.Data;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// The Program class contains the Main function for the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main function for the program.
        /// </summary>
        /// <param name="args">arguments for the main program.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // Get connection string
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                    ?? builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IShortURLRepository, ShortURLRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbContext.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}