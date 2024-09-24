using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Data.ApiClient;
using NetReactMovie.Server.Repositories.Impementations;
using NetReactMovie.Server.Services.Implementations;
using NetReactMovie.Server.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add appsetting to config options
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();

//Get and set db and http config variables
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var omdbApiKey = builder.Configuration["OMDB:ApiKey"];
var omdbBaseUrl = builder.Configuration["OMDB:BaseUrl"];

// Register HttpClient with IHttpClientFactory
builder.Services.AddHttpClient("HTTPClient", client =>
{
    client.BaseAddress = new Uri($"{omdbBaseUrl}"); // OMDB base URL
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
//Register MovieRepository
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<IOmdbClient, OmdbClient>();
// Register IMovieService and MovieService
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//configure options for OMDb API
builder.Services.Configure<OmdbAPI>(options =>
{
    options.ApiKey = omdbApiKey;
    options.BaseUrl = omdbBaseUrl;
});

builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
