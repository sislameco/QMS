using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Db;
using Repository.Seeds;
using Services;
using Services.Interfaces;
using Services.Implementations;
using Utils;
using Utils.EmailUtil;
using WebApi.Extensions;
using WebApi.Middlewares;
using WebApi.Transformers;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<HelpDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Registering HttpAccessor
builder.Services.AddHttpContextAccessor();

// Register the task queue as a singleton
builder.Services.AddSingleton<IMailBackgroundTaskQueue>(_ => new MailBackgroundTaskQueue(100));
builder.Services.AddHostedService<QueuedMailBackgroundService>();

// Register UserService for DI
builder.Services.AddScoped<IUserService, UserService>();

// 🔄 Load origins from config
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();
// 🔐 CORS Policy
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend",
//        policy =>
//        {
//            policy.WithOrigins(allowedOrigins!)
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

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

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();


// Seed Db
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelpDbContext>();
    await DbInitializer.SeedDailyAvailabilityAsync(dbContext);
}

app.Run();
