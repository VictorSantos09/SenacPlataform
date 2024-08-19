using BancoTalentos.API.Context;
using BancoTalentos.API.Handlers;
using BancoTalentos.Domain.Config;
using BancoTalentos.Domain.Exceptions.ImagemConfig;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SenacPlataform.Shared.DependencyInjection;
using SenacPlataform.Shared.Extensions;
using System.Data;
using System.Security.Claims;

namespace BancoTalentos.API.Config;

internal static class BancoTalentosConfig
{
    private const string CNT_CONFIGURACAO_IMAGEM_JSON_SECTION = "ImageConfig";
    public const string CNT_CORS_POLICY_NAME = "SenacPlataformPolicy";
    private static readonly string CNT_PATH_LOGS = $"{Path.GetTempPath()}logs/myapp.txt";
    public static IServiceCollection AddBancoTalentos(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.SPGetConnectionString();

        _ = services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddScoped<IDbConnection>(x => new MySqlConnection(connectionString));
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddIdentityApiEndpoints<ApplicationIdentityUser>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();

        AddConfiguracaoImagem(services, builder);
        ConfigureCookies(services);
        ConfigureAccountRequirements(services);
        ConfigureCORS(services);

        _ = services.AddValidatorsFromAssembly(typeof(BancoTalentosDomainConfig).Assembly, includeInternalTypes: true);
        _ = services.AddDependencies(typeof(BancoTalentosDomainConfig).Assembly);
        _ = services.AddDependencies(typeof(BancoTalentosConfig).Assembly);

        return services;
    }

    /// <summary>
    /// Adiciona a configuração de imagem no container de injeção de dependência.
    /// <para/>
    /// A configuração de imagem é obtida do arquivo appsettings.json.
    /// <para/>
    /// A aplicação não sobe se a configuração não for encontrada ou for inválida.
    /// <para/>
    /// Essa configuração adiciona a instancia em formato scoped. Permitindo o uso da classe <see cref="ImageConfig"/> para acessar as configurações de imagem.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    /// <exception cref="ImageConfigurationNotFoundException">Caso não seja encontrada a seção de configuração.</exception>
    /// <exception cref="ImageConfigurationInvalidException">Caso alguma das configurações seja inválida. </exception>
    private static void AddConfiguracaoImagem(IServiceCollection services, WebApplicationBuilder builder)
    {
        var imageConfig = builder.Configuration.GetSection(CNT_CONFIGURACAO_IMAGEM_JSON_SECTION).Get<ImageConfig>()
            ?? throw new ImageConfigurationNotFoundException(CNT_CONFIGURACAO_IMAGEM_JSON_SECTION, $"Exceção lançada em {nameof(BancoTalentosConfig)}.cs");

        CheckImageConfiguration(imageConfig);
        services.AddScoped(x => imageConfig);
    }

    private static void CheckImageConfiguration(ImageConfig imageConfig)
    {
        var validationResult = ImageConfig.Validate(imageConfig);

        if (validationResult.IsFailed)
        {
            throw new ImageConfigurationInvalidException(CNT_CONFIGURACAO_IMAGEM_JSON_SECTION, "Verificado os dados do objeto.", validationResult.ToErros());
        }
    }

    #region Security
    private static void ConfigureCookies(IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "SenacPlataform";
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            // necessário confirmar com pessoal de TI
            options.Cookie.SameSite = SameSiteMode.None;
        });
    }

    private static void ConfigureAccountRequirements(IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            /* Necessário confirmar com pessoal do TI, para implementar também é preciso ver com eles por conta das restrições de segurança.*/
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;

            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
        });
    }

    private static void ConfigureCORS(IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy(CNT_CORS_POLICY_NAME, builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        }));
    }

    public static async Task AddDomainRolesAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            List<string> roles = ["ADMIN"];
            List<Claim> claims = [new Claim("ADMIN", "TRUE")];

            IdentityRole role;
            foreach (var r in roles)
            {
                role = new(r);
                if (!await roleManager.RoleExistsAsync(r))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
    #endregion
}
