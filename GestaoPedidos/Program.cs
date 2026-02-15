using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using GestaoPedidos.Application.Mapper;
using GestaoPedidos.Domain.Abstractions;
using GestaoPedidos.Infrastructure.Data;
using GestaoPedidos.Infrastructure.Middlewares;
using GestaoPedidos.Infrastructure.Repositories;
using GestaoPedidos.Application.Validators.Clientes;


var builder = WebApplication.CreateBuilder(args);


// ================= CORS =================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ================= CONTROLLERS =================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================= DB CONTEXT =================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ================= REPOSITORIES =================

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();



// ================= AUTOMAPPER =================

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// ================= VALIDATORS =================

builder.Services.AddValidatorsFromAssemblyContaining<ClienteCreateValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddHttpContextAccessor();

// ================= USE CASES (SCAN) =================

builder.Services.Scan(scan => scan
    .FromApplicationDependencies()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
    .AsSelf()
    .WithScopedLifetime());

var app = builder.Build();

// ================= PIPELINE =================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();