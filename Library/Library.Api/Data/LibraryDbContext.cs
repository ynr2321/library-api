using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Checkout> Checkouts => Set<Checkout>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // index / IsRequired wont have an effect using EF Core In Memory Db but just added in case decide to quickly hook up to a db container later
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Isbn);
            entity.Property(b => b.Isbn).HasMaxLength(13);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(300);
            entity.Property(b => b.Author).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Genre).HasMaxLength(100);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(m => m.LastName).IsRequired().HasMaxLength(100);
            entity.Property(m => m.Email).IsRequired().HasMaxLength(200);
            entity.HasIndex(m => m.Email).IsUnique();
        });

        modelBuilder.Entity<Checkout>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.HasOne(c => c.Book)
                .WithMany(b => b.Checkouts)
                .HasForeignKey(c => c.BookIsbn);

            entity.HasOne(c => c.Member)
                .WithMany(m => m.Checkouts)
                .HasForeignKey(c => c.MemberId);
        });
    }
}
