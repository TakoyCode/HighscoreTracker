using Microsoft.Data.SqlClient;
using Dapper;

namespace HighscoreTracker;

public class DB
{

    private SqlConnection _connection;

    public DB(SqlConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<int> UpdateHighscore(Highscore highscore)
    {
        var sql = @"UPDATE Highscores
              SET CurrentStreak = @CurrentStreak, HighScore = @HighScore, TotalDays = @TotalDays WHERE ID = @ID";
        return await _connection.ExecuteAsync(sql, highscore);
    }

    public async Task<int> DeleteHighscore(Highscore highscore)
    {
    var sql = @"DELETE FROM Highscores WHERE ID = @ID";
    return await _connection.ExecuteAsync(sql, highscore);
    }

    public async Task<int> CreateHighscore(NewHighscore highscore)
    {
        var sql = @"INSERT INTO Highscores (CurrentStreak, HighScore, TotalDays)
               VALUES (@CurrentStreak, @HighScore, @TotalDays)";
    return await _connection.ExecuteAsync(sql, highscore);
}

public async Task<IEnumerable<Highscore>> ReadHighscore()
{
    var sql = "SELECT * FROM Highscores";
    return await _connection.QueryAsync<Highscore>(sql);
}
}
