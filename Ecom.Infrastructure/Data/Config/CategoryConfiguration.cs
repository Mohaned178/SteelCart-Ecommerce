using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Id).IsRequired();

            builder.HasData(
                new Category { Id = 1, Name = "Graphics Cards", Description = "All kinds of GPU products" },
                new Category { Id = 2, Name = "Laptops", Description = "Gaming and professional laptops" },
                new Category { Id = 3, Name = "Headphones", Description = "Audio devices and gaming headsets" },
                new Category { Id = 4, Name = "Monitors", Description = "High-resolution and gaming monitors" },
                new Category { Id = 5, Name = "Sports Equipment", Description = "Tools and gear for fitness and sports" }
            );
        }
    }
}
