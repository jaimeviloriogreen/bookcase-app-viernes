using BookcaseApp.Models;
using BookcaseApp.Repository;

namespace BookcaseApp.Services;

public class BookService(BookRepository bookRepository) {
  /// <summary>
  /// Campo privado al cual se le inyecta una referencia al repositorio de libros.
  /// </summary>
  private readonly BookRepository _bookRepository = bookRepository;


  /// <summary>
  /// Este método trae del repositorio  una lista de libros.
  /// </summary>
  /// <returns>Lista de libros</returns>
  public List<Book> FindAll() {
    return _bookRepository.FindAll();
  }

  public bool ExistOne() {
    return _bookRepository.ExistOne();
  }

  public int Delete(int id) {
    return _bookRepository.Delete(id);
  }

  public int Create(string title, string description, string isbn, string datePurchased) {
    return _bookRepository.Create(title, description, isbn, datePurchased);
  }

  public int Update(int id, string title, string description, string isbn, string datePurchased) {
    return _bookRepository.Update(id, title, description, isbn, datePurchased);
  }
}