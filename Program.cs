using Supabase;
using Microsoft.EntityFrameworkCore;
using backEnd.Data;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

//Environment variables loaded to use in the connection string
var DB_HOST = Environment.GetEnvironmentVariable("DB_HOST");
var DB_PORT = Environment.GetEnvironmentVariable("DB_PORT");
var DB = Environment.GetEnvironmentVariable("DB");
var DB_USER = Environment.GetEnvironmentVariable("DB_USER");
var DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");

//environment variables put into a list to iterate over and check they're not null
List<string> envVariables = new List<string>
{
    DB_HOST, DB_PORT, DB, DB_USER, DB_PASSWORD
};

//iterate over them and throw an error if one is empty
foreach (var variable in envVariables)
{
    if (string.IsNullOrEmpty(variable))
    {
        throw new InvalidOperationException($"{variable} env variable is not set");
    }
}

//the actual connection statement
//it also concactenates the string, pasting the full connection string here or in one env variable is not a viable option
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql($"Host={DB_HOST};PORT={DB_PORT};Database={DB};Username={DB_USER};Password={DB_PASSWORD}")
    );

var app = builder.Build();
app.MapGet("/", () =>
{
    var CR = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Not Set";
    return $"Connection String: {CR}";

}
);

app.Run();
