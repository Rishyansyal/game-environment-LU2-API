using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from the configuration
var dbConnectionString = builder.Configuration.GetConnectionString("SqlConnectionString");

// Add services to the container.
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(); // Add authorization
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddDapperStores(options => { options.ConnectionString = dbConnectionString; });

// Adding the HTTP Context accessor to be injected. This is needed by the AspNetIdentityUserRepository
// to resolve the current user.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

// Register IDbConnection with a scoped lifetime
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(dbConnectionString));

// Register the repository
builder.Services.AddScoped<IEnvironment2DRepository, Environment2DRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

// Map the Identity API endpoints under the 'account' prefix
app.MapGroup("/account")
    .MapIdentityApi<IdentityUser>();

var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(dbConnectionString);
app.MapGet("/", () => $"The API is up. Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");

// Secure all controllers and endpoints with RequireAuthorization
app.MapControllers().RequireAuthorization();

app.Run();
