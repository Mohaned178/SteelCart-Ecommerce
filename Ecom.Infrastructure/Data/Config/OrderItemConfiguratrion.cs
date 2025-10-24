using Ecom.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.ProductItemId).IsRequired();
            builder.Property(x => x.MainImage).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasData(
                new OrderItem
                {
                    Id = 1,
                    ProductItemId = 1,
                    MainImage = "https://example.com/images/rtx4090.jpg",
                    ProductName = "NVIDIA RTX 4090",
                    Price = 2000,
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = 2,
                    ProductItemId = 2,
                    MainImage = "https://example.com/images/rx7900xt.jpg",
                    ProductName = "AMD Radeon RX 7900 XT",
                    Price = 1000,
                    Quantity = 2
                },
                new OrderItem
                {
                    Id = 3,
                    ProductItemId = 5,
                    MainImage = "https://example.com/images/football.jpg",
                    ProductName = "Adidas Official Match Football",
                    Price = 90,
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = 4,
                    ProductItemId = 6,
                    MainImage = "https://example.com/images/shoes.jpg",
                    ProductName = "Nike Zoom Running Shoes",
                    Price = 150,
                    Quantity = 1
                }
            );
        }
    }
}
