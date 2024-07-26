using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configurationBuilder = builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .AddCommandLine(args);

// Veritabanı bağlantı dizesini alın
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??"localDb";

// Entity Framework Core kullanarak MSSQL bağlantısını ekleyin
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// CORS'u yapılandırın
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Vue.js uygulamanızın adresi
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Swagger/OpenAPI için servisleri ekleyinz
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Controller servislerini ekleyin
builder.Services.AddControllers();

var app = builder.Build();

// Middleware'leri ekleyin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}




app.UseHttpsRedirection();
app.UseRouting();

// CORS'u kullanın
app.UseCors("AllowSpecificOrigin");

// Authorization ve Authentication işlemlerini ekleyin (eğer gerekiyorsa)
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();

