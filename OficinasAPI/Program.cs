using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OficinasAPI.Business;
using OficinasAPI.Business.Interface;
using OficinasAPI.Config;
using OficinasAPI.Model.Context;
using OficinasAPI.Repository;
using OficinasAPI.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration["MySqlConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options
    .UseMySql(connection,
        new MySqlServerVersion(
            new Version(8, 0, 28))));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IOficinaRepository, OficinaRepository>();
builder.Services.AddTransient<IOficinaBusiness, OficinaBusiness>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
