using System.Text.Json;
using System.Text.Json.Serialization;
using DocBuilder.Data;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services
  .AddDbContext<AppDbContext>(options =>
  {
      options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      options.UseSqlServer(builder.Configuration.GetConnectionString("DocBuilder"));
  })
  .AddControllers()
  .AddOData(options => options.Count().Filter().OrderBy().Select().SetMaxTop(100))
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
  });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(config =>
{
    config.WithOrigins(GetConfigArray(builder.Configuration, "CorsOrigins"))
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithExposedHeaders("Content-Disposition");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

string[] GetConfigArray(ConfigurationManager config, string section) =>
    config.GetSection(section)
        .GetChildren()
        .Select(x => x.Value)
        .ToArray();
