## Consulas con ADO.NET de ejemplo

```c#
using BookcaseApp.Database;
using Microsoft.Data.Sqlite;

// Conexión a la base de datos
var db = new Database("./bookcase.db");
var connection = db.GetConnection();

//? Consulta a la base de datos
// var sql = "SELECT * FROM autor";

// using var command = new SqliteCommand(sql, connection);
// using var reader = command.ExecuteReader();

// reader.Read() -> Evalúa si la consulta devolvió filas.
// Al buscar por una clave primaria como el id, se proceso como un único registro.
// while (reader.Read()) {
//   var id = reader.GetInt32(0);
//   var firstName = reader.GetString(1);
//   var lastName = reader.GetString(2);
//   var nationality = reader.GetString(3);

//   Console.WriteLine($"El autor con id {id} se llama {firstName} {lastName} y es de nacionalidad {nationality}");
// }

//? Inserta registro en la base de datos
// var sql = """
// INSERT INTO autor(
//     nombre,
//     apellido,
//     nacionalidad
//   )
//   VALUES(
//     @nombre,
//     @apellido,
//     @nacionalidad
//   ) RETURNING id_autor;
// """;

// using var command = new SqliteCommand(sql, connection);

// command.Parameters.AddWithValue("@nombre", "Jack");
// command.Parameters.AddWithValue("@apellido", "Foster");
// command.Parameters.AddWithValue("@nacionalidad", "Estadounidense");

// var lastIdInserted = command.ExecuteScalar();

// === OPCIONAL ===
// var rowInserted = command.ExecuteNonQuery();

// Recuparar el ID generado
// using var commandId = new SqliteCommand("SELECT last_insert_rowid();", connection);
// var lastIdInserted = commandId.ExecuteScalar();

// Console.WriteLine($"Fueron modificadas las siguientes filas: {rowInserted}");
// === END ===

// Console.WriteLine($"El último registro insertado es : {lastIdInserted}");

//? Eliminar un registro en la base de datos

// var sql = "DELETE FROM autor WHERE id_autor = @id_autor";
// int id = 4;

// using var command = new SqliteCommand(sql, connection);
// command.Parameters.AddWithValue("@id_autor", id);

// var rowDeleted = command.ExecuteNonQuery();
// Console.WriteLine($"El autor con el id {id} fue eliminado.\nFilas afectadas: {rowDeleted}.");

//? Actualizar un registro
var sql = """
    UPDATE autor SET
      nombre = @nombre,
      apellido = @apellido,
      nacionalidad = @nacionalidad
    WHERE id_autor = @id;
""";
int id = 4;

using var command = new SqliteCommand(sql, connection);

command.Parameters.AddWithValue("@nombre", "Jostein");
command.Parameters.AddWithValue("@apellido", "Gaarder");
command.Parameters.AddWithValue("@nacionalidad", "Noruega");
command.Parameters.AddWithValue("@id", id);

var rowUpdated = command.ExecuteNonQuery();
Console.WriteLine($"El autor con el id {id} ha sido actualizado.");
```
