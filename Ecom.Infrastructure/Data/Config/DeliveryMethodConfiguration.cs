using Ecom.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(m => m.Price).HasColumnType("decimal(18,2)");

            builder.HasData(
                new DeliveryMethod
                {
                    Id = 1,
                    Name = "DHL Express",
                    Price = 15.00m,
                    DeliveryTime = "3-5 business days",
                    Description = "Fast international shipping with tracking and insurance."
                },
                new DeliveryMethod
                {
                    Id = 2,
                    Name = "FedEx Priority",
                    Price = 18.50m,
                    DeliveryTime = "2-4 business days",
                    Description = "Reliable worldwide delivery with door-to-door service."
                },
                new DeliveryMethod
                {
                    Id = 3,
                    Name = "UPS Ground",
                    Price = 10.00m,
                    DeliveryTime = "5-7 business days",
                    Description = "Affordable local delivery for domestic orders."
                },
                new DeliveryMethod
                {
                    Id = 4,
                    Name = "Aramex Standard",
                    Price = 12.75m,
                    DeliveryTime = "4-6 business days",
                    Description = "Trusted shipping in the Middle East and Europe."
                }
            );
        }
    }
}
