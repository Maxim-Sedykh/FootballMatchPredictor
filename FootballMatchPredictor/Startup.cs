using Microsoft.AspNetCore.Authentication.Cookies;

namespace FootballMatchPredictor
{
    public static class Startup
    {
        /// <summary>
        /// Подключение cookie авторизации и аутентификации
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthorization();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Auth/Login");
                    options.AccessDeniedPath = new PathString("/Auth/Login");
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.Name = "FootballMatchPredictorCookie";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
    }
}
