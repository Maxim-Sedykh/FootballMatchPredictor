using FootballMatchPredictor;
using FootballMatchPredictor.Persistence.DependencyInjection;
using FootballMatchPredictor.Application.DependencyInjection;
using Serilog;
using FootballMatchPredictor.Application.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthenticationAndAuthorization(builder);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
