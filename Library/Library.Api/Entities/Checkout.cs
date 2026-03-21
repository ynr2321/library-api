namespace Library.Api.Entities;

public class Checkout
{
    public int Id { get; set; }
    public string BookIsbn { get; set; } = string.Empty;
    public Guid MemberId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime? ReturnDate { get; set; }  // not using this at the moment, but would be useful if we added 'return book' functionality

    public Book Book { get; set; } = null!;
    public Member Member { get; set; } = null!;
}
