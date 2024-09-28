using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Data.ApiClient;
using NetReactMovie.Server.Repositories.Impementations;
using NetReactMovie.Server.Services.Implementations;
using NetReactMovie.Server.Services.Interfaces;
using NetReactMovie.Server.Repositories.Implementations;
using NetReactMovie.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using NetReactMovie.Server.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Identity services and configure default token providers
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add the JwtService for generating tokens
builder.Services.AddScoped<IJwtService, JwtService>();

// Allow CORS
// Enable CORS to allow specific origins (e.g., your frontend URL)

var myCorPolicy = "AllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name : myCorPolicy,
        policy => policy.WithOrigins("https://localhost:5174", "https://localhost:5173").WithMethods("PUT", "DELETE", "GET", "POST", "PATCH").SetIsOriginAllowedToAllowWildcardSubdomains()                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        );
});
// Enable CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyHeader()
//                        .AllowAnyMethod());
//});



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

builder.Services.AddMemoryCache();

//Register Repositories

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieQueryRepository, MovieQueryRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


// Register IMovieService and MovieService
builder.Services.AddScoped<IOmdbClient, OmdbClient>();
builder.Services.AddScoped<IMovieService, MovieService>();





//Context
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

app.UseCors(myCorPolicy);

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
