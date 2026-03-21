namespace Library.Api.Entities;

public class Member
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime MemberSince { get; set; }

    public ICollection<Checkout> Checkouts { get; set; } = [];
}
