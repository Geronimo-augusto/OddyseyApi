using OddyseyApi.Data;
using OddyseyApi.Data.Interfaces;
using OddyseyApi.Data.Repositories;
using OddyseyApi.Profiles;
using OddyseyApi.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



// ==========================================
// CONFIGURAÇÕES DA API E FERRAMENTAS
// ==========================================

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<TelemetryProfile>();
}, typeof(Program).Assembly);
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

// 1. Configurar Banco de Dados (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// 3. HttpClientFactory (Comunicação com o Python)
builder.Services.AddHttpClient("PythonMLApi", client =>
{
    // A URL base idealmente viria do appsettings.json
    client.BaseAddress = new Uri(builder.Configuration["MlApiSettings:BaseUrl"] ?? "http://localhost:8000/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// 4. Injeção de Dependências (Repositories e Services)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITelemetryPredictionService, TelemetryPredictionService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Nome",
        Version = "v1",
        Description = "API Gateway / BFF para orquestração de serviços e raspagem de dados."
    });

    // Código para Carroregar os comentários XML do Controller
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    
    if (System.IO.File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
    
});








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<OddyseyApi.Hubs.AlertHub>("/hubs/alerts");

app.Run();
