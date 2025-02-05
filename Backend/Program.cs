using Microsoft.Data.SqlClient;
using HighscoreTracker;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

Env.Load();

string connectionString = $@"Server={Environment.GetEnvironmentVariable("SERVER")};
                            Database={Environment.GetEnvironmentVariable("DATABASE")};
                            User Id={Environment.GetEnvironmentVariable("USERID")};
                            Password={Environment.GetEnvironmentVariable("PASSWORD")};
                            Encrypt=False;";


var connection = new SqlConnection(connectionString);

DB db = new DB(connection);

app.MapGet("/Highscores", async () => {
    return await db.ReadHighscore();
    });

app.MapPut("/Highscores", async ([FromBody]Highscore highscore) => {
    return await db.UpdateHighscore(highscore);
    });

app.MapPost("/Highscores", async ([FromBody]NewHighscore highscore) => {
    return await db.CreateHighscore(highscore);
    });

app.MapDelete("/Highscores", async ([FromBody]Highscore highscore) => {
    return await db.DeleteHighscore(highscore);
    });

app.UseHttpsRedirection();

app.Run();
