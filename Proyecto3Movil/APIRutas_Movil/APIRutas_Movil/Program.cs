using APIRutas_Movil.BLL;
using APIRutas_Movil.IBLL;
using APIRutas_Movil.Dapper;
using APIRutas_Movil.IDapper;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.RepositorySQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IDapperContext, DapperContext>();

//Repositorios
builder.Services.AddSingleton<IChoferRepository, ChoferRepository>();
builder.Services.AddSingleton<IRutasRepository, RutasRepository>();
builder.Services.AddSingleton<IIncidenteRepository, IncidenteRepository>();

//BLL
builder.Services.AddSingleton<IChoferBLL, ChoferBLL>();
builder.Services.AddSingleton<IRutasBLL, RutasBLL>();
builder.Services.AddSingleton<IIncidenteBLL, IncidenteBLL>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
