using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Domain.HistoricAggregate;
using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data;
using Motorcycle.Infra.Repository;
using Microsoft.Data.SqlClient;
using Motorcycle.Application.Historic.Services;
using Motorcycle.Application.Users.Services;
using Motorcycle.Application.Motorcycle.Services;
using Motorcycle.Application.Budget.Services;
using System.Net.Http.Headers;
using Motorcycle.Infra.Data.ExternalServices;
using Motorcycle.Infra.ExternalServices;


namespace Motorcycle.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void AddLocalHttpClients(this IServiceCollection services, IConfiguration configuration)
        {

        }
            public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();

            #endregion

            #region Services
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<IBudgetService, BudgetService>();

            services.AddScoped<IEmailService, EmailService>(); 

            #endregion
        }

        public static void AddLocalUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider()
                    .DisableLogging(true)
                    .UseCacheKeyPrefix("EF_"));

            var connString = Builders.BuildConnectionStringACS(configuration);

            services.AddScoped<IUnitOfWork>(serviceProvider =>
            {
                var connection = new DbContextOptionsBuilder<ContextDb>();
                var conn = connString;

                return new UnitOfWork(
                    new ContextDb(
                        connection
                            .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>())
                            .UseLazyLoadingProxies()
                            .UseSqlServer(conn).Options));
            });

            services.AddPooledDbContextFactory<ContextDb>(options => options.UseLazyLoadingProxies().UseSqlServer(connString).EnableSensitiveDataLogging());
        }
    }
    public static class Builders
    {
        public static string BuildConnectionStringACS(IConfiguration configuration)
        {
            // Partial Connection; only host,user,password
            var sqlBuilder = new SqlConnectionStringBuilder(configuration["App:Settings:ConnectionStringPartial"])
            {
                PersistSecurityInfo = true,
                InitialCatalog = "TccLeandroKuranaga",
                MultipleActiveResultSets = true
            };
            return sqlBuilder.ConnectionString;
        }
    }
}
