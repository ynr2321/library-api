namespace Library.Api.Entities;

public class Book
{
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string Genre { get; set; } = string.Empty;

    public ICollection<Checkout> Checkouts { get; set; } = []; // went with collection instead of current checkout because could add return book in future
}
