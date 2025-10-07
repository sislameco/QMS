using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.AppSettings;
using Repository;
using Repository.Db;
using Repository.Repo.UserManagement;
using Repository.Seeds;
using Services;
using Services.AuthService;
using Services.UserManagement;
using Utilities.Redis;
using Utils;
using Utils.Auth;
using Utils.EmailUtil;
using Utils.Integration;
using Utils.Integration.API;
using Utils.LoginData;
using WebApi.Configuration;
using WebApi.Extensions;
using WebApi.Helper;
using WebApi.Middlewares;
using WebApi.Services.Security;
using WebApi.Transformers;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var env = builder.Environment;

// Load custom config
var appConfiguration = env.GetAppConfiguration();
builder.Configuration.AddConfiguration(appConfiguration);
var configuration = builder.Configuration; // after AddConfiguration(appConfiguration)
// One-liner: bind AppProperties section into AppSettings and register as singleton
builder.Services.AddSingleton(
    builder.Configuration.GetSection("AppProperties").Get<AppSettings>()
);


builder.Services.AddDbContext<HelpDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var ddd = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Service Registrations
builder.Services.RegisterServices(
    typeof(IBaseService).Assembly,
    typeof(IBaseUtil).Assembly
);

// Register Generic Repository + UnitOfWork
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IHRService, HRService>();
builder.Services.AddScoped<IQsClient, QsClient>();
builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddAllServices(typeof(IPermissionService).Assembly);
builder.Services.AddAllRepositories(typeof(IDashboardRepository).Assembly);


// Registering HttpAccessor
builder.Services.AddHttpContextAccessor();

// Register the task queue as a singleton
builder.Services.AddSingleton<IMailBackgroundTaskQueue>(_ => new MailBackgroundTaskQueue(100));
builder.Services.AddHostedService<QueuedMailBackgroundService>();

// Register UserService for DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Redis configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    // Use configuration binding instead of static property
    var redisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value;
    options.Configuration = redisConnectionString;
});
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddSingleton<IUserInfos, UserInfos>();
builder.Services.AddBearerHeader();
var corsOrigins = builder.Configuration.GetSection("AppProperties:CorsOrigins").Get<string[]>();
builder.Services.AddMvc(options =>{options.Filters.Add<AuthFilter>();});

// ✅ Use the CORS policy


// 🔄 Load origins from config
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

// Add CORS policy using CorsOrigins from config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AppCorsPolicy", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

// 1. Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Building Web Application
var app = builder.Build();

// Register Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

// Route Conf
app.UsePathBase("/api");
app.Use((context, next) =>
{
    context.Request.PathBase = "/api";
    return next();
});
app.UseRouting();

// Swagger Conf
if (app.Environment.IsDevelopment())
{
    // 🔧 Enable Swagger UI in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger";
    });
}
// ✅ Use the CORS policy
app.UseCors("AllowAll");

app.UseCors("AllowFrontend");

var cacheService = app.Services.GetRequiredService<ICacheService>();
AuthCacheUtil.RedisCacheInitialize(cacheService);

app.UseAuthorization();

app.MapControllers();


// Seed Db
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelpDbContext>();
    await DbInitializer.SeedDailyAvailabilityAsync(dbContext);
}

app.Run();
