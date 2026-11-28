using BookcaseApp.Connection;
using BookcaseApp.Repository;
using BookcaseApp.Screens;
using BookcaseApp.Services;

var db = new Database("Database/bookcase.db");
var bookRepository = new BookRepository(db);
var bookService = new BookService(bookRepository);

var screen = new MainScreen(bookService);
screen.ShowUI();