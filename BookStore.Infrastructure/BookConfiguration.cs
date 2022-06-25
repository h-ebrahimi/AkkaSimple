using ClusterBookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClusterBookStore.Infrastructure
{
    partial class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.Property(p => p.Id);
            builder.Property(p => p.Price);
            builder.Property(p => p.Title);
            builder.Property(p => p.Author);

            builder.HasKey(k => k.Id);
        }
    }
}