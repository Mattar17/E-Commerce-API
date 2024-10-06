using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIS.G02.Errors;
using Talabat.APIS.G02.Extensions;
using Talabat.APIS.G02.Helpers;
using Talabat.APIS.G02.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIS.G02
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<G02DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AppServices();
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddIdentityServices(builder.Configuration);
            #endregion
            #region Project Build
            var app = builder.Build();
            #endregion
            #region Update-Database

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var DbContext = Services.GetRequiredService<G02DbContext>();
                await DbContext.Database.MigrateAsync();

                var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();

                await G02DbContextSeed.SeedAsync(DbContext);

                var UserManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentitySeed.AppUserSeed(UserManager);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error! Database Not Updated");
            }

            #endregion


            // Configure the HTTP request pipeline.
            #region Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();  
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
