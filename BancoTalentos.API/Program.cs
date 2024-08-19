using BancoTalentos.API.Config;
using BancoTalentos.API.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBancoTalentos(builder);

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapIdentityApi<ApplicationIdentityUser>();

// aguardando correção https://github.com/dotnet/aspnetcore/issues/51888
app.UseExceptionHandler(o => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(BancoTalentosConfig.CNT_CORS_POLICY_NAME);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//await BancoTalentosConfig.AddDomainRolesAsync(app);

app.Run();