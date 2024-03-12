using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Persistence.Interceptor;
using FootballMatchPredictor.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            {
                options.UseNpgsql(connectionString);
            });
            services.InitRepositories();
        }

        private static void InitRepositories(this IServiceCollection services)
        {
            var types = new List<Type>()
            {
                typeof(Bet),
                typeof(BetType),
                typeof(Coefficient),
                typeof(User),
                typeof(Country),
                typeof(Match),
                typeof(Team),
            };

            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseRepository<>).MakeGenericType(type);
                var implementationType = typeof(BaseRepository<>).MakeGenericType(type);
                services.AddScoped(interfaceType, implementationType);
            }
        }
    }
}
