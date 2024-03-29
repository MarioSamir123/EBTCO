﻿using EBTCO.Core.Contract.DBRepo;
using EBTCO.DB;
using EBTCO.RDS.Implementation;
using Microsoft.EntityFrameworkCore;
using ToursYard.RDS.Implementation;

namespace EBTCO.ServicesRegisteration
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetSection("ebtco_db").Value ?? String.Empty;

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