using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Interfaces.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Внедрение зависимостей слоя Application
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplication(this IServiceCollection services)
        {
            InitServices(services);

            InitMapping(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IBetService, BetService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ICoefficientService, CoefficientService>();
        }

        private static void InitMapping(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
