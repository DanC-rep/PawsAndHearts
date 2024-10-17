using PawsAndHearts.Accounts.Application;
using PawsAndHearts.Accounts.Infrastructure;
using PawsAndHearts.BreedManagement.Application;
using PawsAndHearts.BreedManagement.Infrastructure;
using PawsAndHearts.BreedManagement.Presentation;
using PawsAndHearts.PetManagement.Application;
using PawsAndHearts.PetManagement.Infrastructure;
using PawsAndHearts.PetManagement.Presentation;
using PawsAndHearts.Web.Extensions;
using Serilog;
using Serilog.Events;

namespace PawsAndHearts.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging(builder.Configuration);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwaggerGen();

        builder.Services
            .AddAccountsModule(builder.Configuration)
            .AddBreedManagementModule(builder.Configuration)
            .AddPetManagementModule(builder.Configuration);

        builder.Services.AddAuthServices(builder.Configuration);

        var app = builder.Build();

        app.UseExceptionMiddleware();

        app.UseSerilogRequestLogging();
            
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
            
        app.MapControllers();

        await app.RunAsync();
    }
}