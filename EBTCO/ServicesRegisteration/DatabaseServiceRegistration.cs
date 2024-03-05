using Microsoft.EntityFrameworkCore;
using EBTCO.DB;

namespace EBTCO
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetConnectionString("mssql") ?? String.Empty;

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
            return services;
        }
    }
}