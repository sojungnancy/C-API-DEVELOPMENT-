using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using A2TEMPLATE.Data;
using A2TEMPLATE.Handler;
using System.Security.Claims;
using A2TEMPLATE.Helper;

namespace A2TEMPLATE
{
    public class Program
    {
        public static void Main(string[] args){

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("A2Authentication", null);

            builder.Services.AddDbContext<A2DbContext>(options => options.UseSqlite(builder.Configuration["A2DBConnection"]));

            builder.Services.AddAuthorization(options => 
            {
                options.AddPolicy("OrganizerPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Organizer"));
                options.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "NormalUser"));
                options.AddPolicy("AuthOnly", policy => 
                {policy.RequireAssertion(
                    context => context.User.HasClaim(c => c.Value == "NormalUser" || c.Value == "Organizer"));
                });
            }
            );

            builder.Services.AddMvc(options => options.OutputFormatters.Add(new CalendarOutputFormatter()));

            builder.Services.AddScoped<IA2Repo, A2Repo>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    

    }
}