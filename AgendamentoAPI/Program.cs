using AgendamentoAPI.Business;
using AgendamentoAPI.Business.Interface;
using AgendamentoAPI.Config;
using AgendamentoAPI.Model.Context;
using AgendamentoAPI.Repository;
using AgendamentoAPI.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication
                 (JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,

                         ValidIssuer = builder.Configuration["Jwt:Issuer"],
                         ValidAudience = builder.Configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                     };
                 });

var connection = builder.Configuration["MySqlConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options
    .UseMySql(connection,
        new MySqlServerVersion(
            new Version(8, 0, 28))));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IAgendamentoBusiness, AgendamentoBusiness>();
builder.Services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddTransient<IOficinaRepository, OficinaRepository>();
builder.Services.AddTransient<IOficinaBusiness, OficinaBusiness>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
