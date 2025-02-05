using Microsoft.Data.SqlClient;
using Dapper;
using HighscoreTracker;
using DotNetEnv;

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

Console.WriteLine(connectionString);

var connection = new SqlConnection(connectionString);

app.MapGet("/Highscores", async () => {
    return await ReadHighscore();
    });

app.MapPut("/Highscores", async (Highscore highscore) => {
    return await UpdateHighscore(highscore);
    });

app.MapPost("/Highscores", async (NewHighscore highscore) => {
    return await CreateHighscore(highscore);
    });

async Task<int> UpdateHighscore(Highscore highscore)
{
    var sql = @"UPDATE Highscores
              SET CurrentStreak = @CurrentStreak, HighScore = @HighScore, TotalDays = @TotalDays WHERE ID = @ID";
    return await connection.ExecuteAsync(sql, highscore);
}

async Task<int> CreateHighscore(NewHighscore highscore)
{
    var sql = @"INSERT INTO Highscores (CurrentStreak, HighScore, TotalDays)
               VALUES (@CurrentStreak, @HighScore, @TotalDays)";
    return await connection.ExecuteAsync(sql, highscore);
}

async Task<IEnumerable<Highscore>> ReadHighscore()
{
    var sql = "SELECT * FROM Highscores";
    return await connection.QueryAsync<Highscore>(sql);
}

app.UseHttpsRedirection();

app.Run();
