using DailyScriptureApi.Data;
using DailyScriptureApi.EndPoints;
using DailyScriptureApi.Services.Interface;
using DailyScriptureApi.Services.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});



builder.Services.AddScoped<IVerseRepository, VerseRepository>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapScalarApiReference(options => 
    {
        options.Title = "Daily Scripture API";
        options.Theme = ScalarTheme.DeepSpace;
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapVerseEndPoint();

app.UseCors();

app.Run();

