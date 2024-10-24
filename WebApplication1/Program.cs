using Application;
using Application.Interfaces;
using Application.Mapping;
using Application.Models;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Middleware;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IDto>()); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<IDbContext, AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IPaymentManager, PaymentManager>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<TokenBlacklistService>();

string plainPassword = "password";

string passwordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);


var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("jwt:secret_key").Value);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

app.UseMiddleware<TokenBlacklistMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
