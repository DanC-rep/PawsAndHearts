using PawsAndHearts.API.Extensions;
using PawsAndHearts.Application;
using PawsAndHearts.Infrastructure;
using Serilog;
using Serilog.Events;

namespace PawsAndHearts.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                             ?? throw new ArgumentNullException("Seq"))
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSerilog();

            builder.Services
                .AddInfrastructure()
                .AddApplication()
                .AddAPI();

            var app = builder.Build();

            app.UseExceptionMiddleware();

            app.UseSerilogRequestLogging();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.ApplyMigration();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
