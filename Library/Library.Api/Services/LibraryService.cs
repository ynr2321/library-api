using Library.Api.DTOs;
using Library.Api.Entities;
using Library.Api.Enums;
using Library.Api.Repositories;

namespace Library.Api.Services;

public class LibraryService : ILibraryService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly ICheckoutRepository _checkoutRepository;

    public LibraryService(
        IBookRepository bookRepository,
        IMemberRepository memberRepository,
        ICheckoutRepository checkoutRepository)
    {
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
        _checkoutRepository = checkoutRepository;
    }

    public async Task<List<BookResponseDto>> GetAllBooksAsync()
    {
        // get all books
        List<Book> books = await _bookRepository.GetAll();

        // map each of the books to a DTO, checking if it's available
        return books.Select(b =>
        {
            Checkout? activeCheckout = b.Checkouts.FirstOrDefault(); // if we added return book functionality, would need to return date here
            return new BookResponseDto(
                Isbn: b.Isbn,
                Title: b.Title,
                Author: b.Author,
                PublishedYear: b.PublishedYear,
                Genre: b.Genre,
                IsAvailable: activeCheckout == null,
                CheckedOutByMemberId: activeCheckout?.MemberId);
        }).ToList();
    }

    public async Task<MemberBooksResponseDto?> GetMemberBooksAsync(Guid memberId)
    {
        Member? member = await _memberRepository.GetByIdAsync(memberId);

        if (member is null) return null;

        // Get member's checkouts  and map them to MemberBookDtos
        List<Checkout> checkouts = await _checkoutRepository.GetActiveCheckoutsByMemberAsync(memberId);

        List<MemberBookDto> checkedOutBooks = checkouts
            .Select(c => new MemberBookDto(
                Isbn: c.Book.Isbn,
                Title: c.Book.Title,
                Author: c.Book.Author,
                CheckoutDate: c.CheckoutDate))
            .ToList();

        return new MemberBooksResponseDto(
            MemberId: member.Id,
            MemberName: $"{member.FirstName} {member.LastName}",
            Books: checkedOutBooks);
    }


    public async Task<CheckoutResult> CheckoutBookAsync(CheckoutRequestDto request)
    {
        Book? book = await _bookRepository.GetByIsbnAsync(request.BookIsbn);
        if (book is null)
        {
            return new CheckoutResult(success: false, checkout: null, error: CheckoutError.BookNotFound);
        }

        Member? member = await _memberRepository.GetByIdAsync(request.MemberId);
        if (member is null)
        {
            return new CheckoutResult(success: false, checkout: null, error: CheckoutError.MemberNotFound);
        }

        Checkout? existing = await _checkoutRepository.GetActiveCheckoutForBookAsync(request.BookIsbn);
        if (existing is not null)
        {
            return new CheckoutResult(success: false, checkout: null, error: CheckoutError.BookAlreadyCheckedOut);
        }

        Checkout checkout = new Checkout
        {
            BookIsbn = request.BookIsbn,
            MemberId = request.MemberId,
            CheckoutDate = DateTime.UtcNow,
            Book = book,
            Member = member
        };

        Checkout created = await _checkoutRepository.CreateAsync(checkout);

        return new CheckoutResult(success: true, checkout: MapToDto(created), error: null);
    }

    private static CheckoutResponseDto MapToDto(Checkout checkout)
    {
        return new(Id: checkout.Id,
                   BookIsbn: checkout.BookIsbn,
                   BookTitle: checkout.Book.Title,
                   MemberId: checkout.MemberId,
                   MemberName: $"{checkout.Member.FirstName} {checkout.Member.LastName}",
                   CheckoutDate: checkout.CheckoutDate);

    }
}
