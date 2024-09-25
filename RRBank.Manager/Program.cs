using Microsoft.EntityFrameworkCore;
using RRBank.Application.Services;
using RRBank.Application.Services.Caching;
using RRBank.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número de tentativas antes de falhar
            maxRetryDelay: TimeSpan.FromSeconds(10), // Atraso máximo entre as tentativas
            errorNumbersToAdd: null // Lista de erros que devem acionar uma nova tentativa, null para usar os padrões
        );
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ManagerService>();
builder.Services.AddScoped<ICachingService, CachingService>();

builder.Services.AddStackExchangeRedisCache(x =>
{
    x.InstanceName = "instance";
    x.Configuration = "redis:6379";
});

var app = builder.Build();

// Enable Swagger for all environments (optional).
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


app.UseRouting();
// app.UseAuthorization();

app.MapControllers();

app.Run();

//void ConfigureServices(WebApplicationBuilder builder)
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    builder.Services.AddDbContext<DataContext>(
//        options =>
//            options.UseSqlServer(connectionString));

//}