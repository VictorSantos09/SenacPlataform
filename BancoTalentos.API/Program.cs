using BancoTalentos.API.Config;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBancoTalentos(builder);
builder.Services.AddScoped<IDbConnection>(x => new MySqlConnection("server=localhost;user id=root;pwd=root;database=gestoredu;"));

var app = builder.Build();

// aguardando correção https://github.com/dotnet/aspnetcore/issues/51888
app.UseExceptionHandler(o => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();