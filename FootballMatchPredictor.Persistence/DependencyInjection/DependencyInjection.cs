using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Interfaces.Database;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Persistence.Interceptor;
using FootballMatchPredictor.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using FootballMatchPredictor.Persistence.Database;

namespace FootballMatchPredictor.Persistence.DependencyInjection
{
    /// <summary>
    /// Внедрение зависимостей слоя Persistence
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString");

            services.AddSingleton<AuditInterceptor>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            InitRepositories(services);

            InitUnitOfWork(services);
        }

        private static void InitRepositories(this IServiceCollection services)
        {
            var types = new List<Type>()
            {
                typeof(Bet),
                typeof(Coefficient),
                typeof(User),
                typeof(Match),
                typeof(Team),
                typeof(Withdrawing)
            };

            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseRepository<>).MakeGenericType(type);
                var implementationType = typeof(BaseRepository<>).MakeGenericType(type);
                services.AddScoped(interfaceType, implementationType);
            }
        }

        private static void InitUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
