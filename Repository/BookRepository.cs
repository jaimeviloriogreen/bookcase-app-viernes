using BookcaseApp.Connection;
using BookcaseApp.Models;
using Microsoft.Data.Sqlite;

namespace BookcaseApp.Repository;

public class BookRepository(Database database) {
  /// <summary>
  /// Campo privado al cual se le inyecta una referencia de la coneción a la base de datos.
  /// </summary>
  private readonly Database _database = database;


  /// <summary>
  /// Este método consulta la base de datos para traer una colección de libros.
  /// </summary>
  /// <returns>Una colección de libros de la base de datos</returns>
  public List<Book> FindAll() {
    // var sql = "SELECT id_libro,titulo, descripcion, isbn, fecha FROM libro";

    using var connection = _database.GetConnection();
    // using var command = new SqliteCommand(sql, connection);

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT id_libro,titulo, descripcion, isbn, fecha FROM libro";

    using var reader = command.ExecuteReader();

    List<Book> books = [];

    while (reader.Read()) {

      books.Add(
        new Book {
          IdBook = reader.GetInt32(0),
          Title = reader.GetString(1),
          Description = reader.GetString(2),
          Isbn = reader.GetString(3),
          DatePurchased = reader.GetString(4)
        }
      );

    }

    return books;
  }

  public bool ExistOne() {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = """
      SELECT EXISTS(SELECT * FROM libro);
    """;

    bool exist = Convert.ToBoolean(command.ExecuteScalar());

    return exist;
  }

  public int Delete(int id) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = "DELETE FROM libro WHERE id_libro = @id";

    command.Parameters.AddWithValue("@id", id);

    return command.ExecuteNonQuery();
  }

  public int Create(string title, string description, string isbn, string datePurchased) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = """
      INSERT INTO libro(titulo, descripcion, isbn, fecha)
      VALUES(@title, @description, @isbn, @datePurchased)
    """;

    command.Parameters.AddWithValue("@title", title);
    command.Parameters.AddWithValue("@description", description);
    command.Parameters.AddWithValue("@isbn", isbn);
    command.Parameters.AddWithValue("@datePurchased", datePurchased);

    return command.ExecuteNonQuery();
  }

  public int Update(int id, string title, string description, string isbn, string datePurchased) {
    using var connection = _database.GetConnection();
    using var command = connection.CreateCommand();

    command.CommandText = """
      UPDATE libro SET
        titulo = @title,
        descripcion = @description,
        isbn = @isbn,
        fecha = @datePurchased
      WHERE id_libro = @id
    """;

    command.Parameters.AddWithValue("@title", title);
    command.Parameters.AddWithValue("@description", description);
    command.Parameters.AddWithValue("@isbn", isbn);
    command.Parameters.AddWithValue("@datePurchased", datePurchased);
    command.Parameters.AddWithValue("@id", id);

    return command.ExecuteNonQuery();
  }
}