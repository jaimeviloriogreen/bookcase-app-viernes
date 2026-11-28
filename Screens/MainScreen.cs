using BookcaseApp.Models;
using BookcaseApp.Services;
using Spectre.Console;

namespace BookcaseApp.Screens;

public class MainScreen(BookService bookService) {
  private readonly BookService _bookService = bookService;

  /// <summary>
  /// Opciones de mi menú, que tendrá un texto que represetna la opción y un número que representa la posición de la opción.
  /// </summary>
  private readonly (string Text, int Value)[] choices = [
    ("1. Mostrar libros", 1),
    ("2. Eliminar libro", 2),
    ("3. Agregar un libro", 3),
    ("4. Actualizar un libro", 4),
    ("5. Salir", 0),
  ];


  public void ShowUI() {
    AnsiConsole.Clear();

    // Un banner de texto grande
    var figlet = new FigletText("Bookcase @app") {
      Color = Color.Green,
      Justification = Justify.Center
    };

    AnsiConsole.Write(figlet);

    // Bucle que muestra el menú de opciones de la aplicación.
    while (true) {
      var prompt = new SelectionPrompt<(string Text, int Value)>()
      .Title("Indica una acción a realizar:")
      .AddChoices(choices)
      .HighlightStyle("Green")
      .WrapAround()
      .UseConverter(c => $"{c.Text}");

      var option = AnsiConsole.Prompt(prompt);

      List<Book> books = _bookService.FindAll();

      switch (option.Value) {
        case 1:
          AnsiConsole.Clear();
          if (!_bookService.ExistOne()) {
            AnsiConsole.MarkupLine("[DarkOrange]¡No hay libros para mostrar![/]");
            break;
          }

          var table = new Table();
          table.AddColumn("Id");
          table.AddColumn("Título");
          table.AddColumn("Descripción");
          table.AddColumn("Isbn");
          table.AddColumn("Fecha de compra");

          // Inyecto valores a las filas de la tabla
          foreach (Book book in books) {
            table.AddRow(
              book.IdBook.ToString(),
              book.Title,
              book.Description,
              book.Isbn,
              book.DatePurchased ?? "Sin fecha"
            );
          }

          // Muestro la tabla
          AnsiConsole.Write(table);
          break;

        case 2:
          AnsiConsole.Clear();
          // Si no existe un libro ejecutará este código en la condicional
          if (!_bookService.ExistOne()) {
            AnsiConsole.MarkupLine("[DarkOrange]¡No hay libros para eliminar![/]");
            break;
          }

          int id = AnsiConsole.Ask<int>("Indica el id que quieres eliminar: ");

          AnsiConsole.WriteLine("");
          var confirmed = AnsiConsole.Confirm("¿Estás seguro?");

          if (confirmed) {
            int deleted = _bookService.Delete(id);

            if (deleted > 0) {
              AnsiConsole.MarkupLine("\n[DarkOliveGreen1_1]Libro eliminado![/]\n");
              break;
            }

            AnsiConsole.MarkupLine("\n[red]Libro no encontrado![/]\n");
          }
          else {
            AnsiConsole.MarkupLine("\n[yellow]Operación cancelada![/]\n");

          }


          break;

        case 3:
          AnsiConsole.Clear();
          string title = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica el título del libro: [/]");
          string description = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica la descripción del libro: [/]");
          string isbn = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica el isbn del libro: [/]");
          string datePurchased = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica la fecha de compra (dd-mm-yyyy): [/]");

          AnsiConsole.WriteLine("");
          var confirmedCreate = AnsiConsole.Confirm("¿Estás seguro?");

          if (confirmedCreate) {
            var created = _bookService.Create(title, description, isbn, datePurchased);

            if (created > 0) {
              AnsiConsole.MarkupLine("\n[DarkOliveGreen1_1]Libro creado![/]\n");
              break;
            }
          }
          else {
            AnsiConsole.MarkupLine("\n[yellow]Operación cancelada![/]\n");
          }
          break;
        case 4:
          AnsiConsole.Clear();
          if (!_bookService.ExistOne()) {
            AnsiConsole.MarkupLine("[DarkOrange]¡No hay libros para actualizar![/]");
            break;
          }

          int idUpdate = AnsiConsole.Ask<int>("Indica el id que quieres eliminar: ");
          string titleUpdate = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica el título del libro: [/]");
          string descriptionUpdate = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica la descripción del libro: [/]");
          string isbnUpdate = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica el isbn del libro: [/]");
          string datePurchasedUpdate = AnsiConsole.Ask<string>("[DarkGoldenrod]Indica la fecha de compra (dd-mm-yyyy): [/]");

          AnsiConsole.WriteLine("");

          var confirmedUpdate = AnsiConsole.Confirm("¿Estás seguro?");

          if (confirmedUpdate) {
            var updated = _bookService.Update(idUpdate, titleUpdate, descriptionUpdate, isbnUpdate, datePurchasedUpdate);

            if (updated > 0) {
              AnsiConsole.MarkupLine("\n[DarkOliveGreen1_1]Libro actualizado![/]\n");
              break;
            }
          }
          else {
            AnsiConsole.MarkupLine("\n[yellow]Operación cancelada![/]\n");
          }

          break;
        case 0:
          AnsiConsole.Clear();
          AnsiConsole.MarkupLine("[green]Fin de la aplicación.[/]");
          return;
      }
    }
  }
}