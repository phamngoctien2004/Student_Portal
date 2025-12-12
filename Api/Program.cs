using Amazon.S3;
using Api.Extentions;
using Api.Middlewares;
using Application.IRepository;
using Application.IServices;
using Application.Mappers;
using Application.Services;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ appsettings.json
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
	.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Fatal) // tắt exception nhả tên lỗi dài
    .CreateLogger();

// Gắn Serilog vào host
builder.Host.UseSerilog();

// cấu hình kết nối db
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString);

// cấu hình minio
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = builder.Configuration["AWS:ServiceURL"],
        ForcePathStyle = true, 
    };

    return new AmazonS3Client(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"],
        config
    );
});


builder.Services.AddControllers();
// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddHttpContextAccessor();

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()       // <== Cho phép OPTIONS, GET, POST...
                  .AllowCredentials();    // Nếu bạn dùng cookie/token
        });
});
AppSettings.Initialize(builder.Configuration);

var a = AppSettings.InternalToken;
var a1 = AppSettings.Service1;

builder.Services.AddExceptionHandler<BussinessExceptionHandler>(); // Đăng ký handler global
builder.Services.AddProblemDetails();
builder.Services.AddIdentityService(builder.Configuration); // Đăng ký dịch vụ xác thực và ủy quyền
var app = builder.Build();

await Init(app);
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}
app.UseExceptionHandler();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

Log.Logger.Information($"Service Started");

app.Run();


async Task Init(WebApplication app)
{
	using (var scope = app.Services.CreateScope())
	{
		var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	}
}