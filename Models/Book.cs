namespace BookcaseApp.Models;

public class Book {
  public int IdBook { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required string Isbn { get; set; }
  public string? DatePurchased { get; set; }
}