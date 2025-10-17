using GymManagementBLL;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #region Types of (DI) services
            #region Singleton
            //builder.Services.AddSingleton<IGenericRepository<>, GenericRepository<>>();

            ////service is created only once for the entire application and is shared by all 
            //// the components that request it. 
            //// Useful for services that need to maintain a global state or provide a common functionality, 
            ////  such as logging, configuration, or caching.
            #endregion

            #region Scoped
            //builder.Services.AddScoped<IGenericRepository<>, GenericRepository<>>();

            ////service is created once per request (such as an HTTP request) 
            //// and is disposed at the end of the request. 
            ////useful for services that need to access request-specific data or resources, 
            //// such as database context, user information, or configuration settings.
            #endregion

            #region Transient
            //builder.Services.AddTransient<IGenericRepository<>, GenericRepository<>>();

            ////service is created every time it is requested and is disposed 
            //// as soon as it is no longer needed. 
            ////useful for services that are lightweight and stateless, 
            //// such as validators, mappers, or calculators.
            #endregion
            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            var app = builder.Build();

            #region Data Seeding
            using var scope = app.Services.CreateScope();
            var gymDbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            GymDataSeeding.SeedData(gymDbContext);
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
