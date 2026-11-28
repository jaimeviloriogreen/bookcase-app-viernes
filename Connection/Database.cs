using Microsoft.Data.Sqlite;
namespace BookcaseApp.Connection;

public class Database(string dbPath) {
  private readonly string _stringConnection = $"Data Source={dbPath}";


  /// <summary>
  /// Este método se conecta a la base de datos SQLite.
  /// </summary>
  /// <returns>Una conexión a SQLite abierta.</returns>
  public SqliteConnection GetConnection() {
    var connection = new SqliteConnection(_stringConnection);
    connection.Open();

    return connection;
  }
}