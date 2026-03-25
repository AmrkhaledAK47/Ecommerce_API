using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DAL
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(d => d.Id);
            builder.
                HasMany(d => d.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
