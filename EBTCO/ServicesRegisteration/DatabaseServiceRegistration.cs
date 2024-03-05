using Microsoft.EntityFrameworkCore;
using EBTCO.DB;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.RDS.Implementation;
using ToursYard.RDS.Implementation;

namespace EBTCO
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetSection("mssql").Value ?? String.Empty;

            services.AddDbContext<AppDbContext>((options =>
                options.UseSqlServer(
                    connectionString: ConnectionString,
                    b =>
                    {
                        b.MigrationsAssembly("EBTCO");
                        b.MigrationsAssembly("EBTCO.DB");
                    }
                )
            ));
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}