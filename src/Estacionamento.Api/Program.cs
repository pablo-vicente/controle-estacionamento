using System.Reflection;
using Estacionamento.Application.Interfaces;
using Estacionamento.Application.Services;
using Estacionamento.Data;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ILocacaoAppService, LocacaoAppService>();
builder.Services.AddScoped<ICondutorAppService, CondutorAppService>();
builder.Services.AddScoped<IVeiculoAppService, VeiculoAppService>();
builder.Services.AddScoped<IPoliticaPrecoAppService, PoliticaPrecoAppService>();

builder.Services.AddScoped<ILocacaoService, LocacaoService>();
builder.Services.AddScoped<ICondutorService, CondutorService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IPoliticaPrecoService, PoliticaPrecoService>();


builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString, options =>
    {
        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<EstacionamentoDbContext>();
db.Database.Migrate();