using Microsoft.Data.Sqlite;
namespace BookcaseApp.Database;

class Database(string dbPath) {
  private readonly string _stringConnection = $"Data Source={dbPath}";

  public SqliteConnection GetConnection() {
    var connection = new SqliteConnection(_stringConnection);
    connection.Open();

    return connection;
  }
}