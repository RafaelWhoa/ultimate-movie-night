using Microsoft.Extensions.Options;
using UltimateMovieNight.Model;
using UltimateMovieNight.Repository;
using UltimateMovieNight.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MovieNightDatabaseSettings>(
    builder.Configuration.GetSection(nameof(MovieNightDatabaseSettings)));
builder.Services.AddSingleton<IMovieNightDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MovieNightDatabaseSettings>>().Value);

//Dependency Injection

builder.Services.AddScoped<IMovieRepository, MovieRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

