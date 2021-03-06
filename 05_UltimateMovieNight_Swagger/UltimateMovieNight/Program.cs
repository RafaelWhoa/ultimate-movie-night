using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using UltimateMovieNight.Business;
using UltimateMovieNight.Business.Implementation;
using UltimateMovieNight.Model;
using UltimateMovieNight.Repository;
using UltimateMovieNight.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.Configure<MovieNightDatabaseSettings>(
    builder.Configuration.GetSection(nameof(MovieNightDatabaseSettings)));
builder.Services.AddSingleton<IMovieNightDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MovieNightDatabaseSettings>>().Value);

builder.Services.AddApiVersioning();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:3001", "http://localhost:3000");
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.0",
        new OpenApiInfo
        {
            Title = "Ultimate Movie Night API",
            Version = "1.0",
            Description = "Ultimate Movie Night's API",
            Contact = new OpenApiContact
            {
                Name = "Rafael Costa",
                Url = new Uri("https://github.com/rafaelwhoa")
            }
        });
});

//Migrations Config

builder.Services.AddMvc();

var connectionString = builder.Configuration.GetSection("MovieNightDatabaseSettings:ConnectionString").Value;

var databaseName = builder.Configuration.GetSection("MovieNightDatabaseSettings:DatabaseName").Value;

var client = new MongoClient(connectionString);

builder.Services.AddSingleton<IMongoClient>(client);

builder.Services.AddMigration(new MongoMigrationSettings
{
    ConnectionString = connectionString,
    Database = databaseName
});

//Dependency Injection

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IMovieBusiness, MovieBusiness>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<IFileBusiness, FileBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1.0/swagger.json",
        "Ultimate Movie Night API 1.0");
});

var option = new RewriteOptions();
option.AddRedirect("ˆ$", "swagger");

app.UseRewriter(option);

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();

