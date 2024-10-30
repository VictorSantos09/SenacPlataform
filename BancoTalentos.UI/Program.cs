using BancoTalentos.Domain;
using BancoTalentos.Domain.Services.Pessoas;
using BancoTalentos.UI.Components;
using BancoTalentos.UI.Components.Account;
using BancoTalentos.UI.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Radzen;
using SenacPlataform.Shared.Config;
using SenacPlataform.Shared.Extensions;
using System.Data;
using BancoTalentos.Domain.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBancoTalentosConfig(builder.Configuration);
builder.Services.AddScoped<IDbConnection>(x => new MySqlConnection(builder.Configuration.SNGetConnectionString()));
builder.Services.SNAddBancoTalentosDomain();
builder.Services.SNAddBancoTalentosShared();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = SystemConfig.SYSTEM_COOKIE_NAME;
    options.Duration = TimeSpan.FromDays(SystemConfig.SYSTEM_COOKIE_EXPIRATION);
});

builder.Services.AddScoped<ThemeService>();

builder.Services.AddRadzenComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.SNGetConnectionString();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
