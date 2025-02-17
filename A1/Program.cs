
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using A1.Data;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Register DbContext with the service container
        builder.Services.AddDbContext<A1DbContext>(options =>
            options.UseSqlite(builder.Configuration["WebAPIConnection"]));
        // Register the implementation of the interface with the service container
        builder.Services.AddScoped<IA1Repo, A1Repo>();

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
