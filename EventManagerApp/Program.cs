using Microsoft.EntityFrameworkCore;
using MonApp.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuration CORS modifiée pour autoriser localhost:7000
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("https://localhost:7000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Important pour les cookies/auth
    });
});

// Services existants
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddDbContext<DbContext, AppDbContext>(
    c => { c.UseSqlite("Data Source=MaDb.db;"); });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments("WebApiDoc.xml");
});

var app = builder.Build();

// Middleware pour configurer les en-têtes de sécurité
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    await next();
});

// Configure le pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

// UseCors avant les autres middleware
app.UseCors();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();