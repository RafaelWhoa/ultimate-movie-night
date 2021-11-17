using Microsoft.Extensions.Options;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using UltimateMovieNight.Business;
using UltimateMovieNight.Business.Implementation;
using UltimateMovieNight.Model;
using UltimateMovieNight.Repository;
using UltimateMovieNight.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MovieNightDatabaseSettings>(
    builder.Configuration.GetSection(nameof(MovieNightDatabaseSettings)));
builder.Services.AddSingleton<IMovieNightDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MovieNightDatabaseSettings>>().Value);

builder.Services.AddApiVersioning();

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

builder.Services.AddScoped<IMovieBusiness, MovieBusiness>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

