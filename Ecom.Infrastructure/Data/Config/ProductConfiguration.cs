using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");

            builder.HasData(
                // ----- Graphics Cards -----
                new Product { Id = 1, Name = "NVIDIA RTX 4090", Description = "High-end gaming GPU with 24GB VRAM", CategoryId = 1, NewPrice = 2000, OldPrice = 2200 },
                new Product { Id = 2, Name = "AMD Radeon RX 7900 XT", Description = "Top-tier AMD GPU for 4K gaming", CategoryId = 1, NewPrice = 1000, OldPrice = 1200 },
                new Product { Id = 3, Name = "NVIDIA RTX 4070 Ti", Description = "Mid-range GPU offering great performance for 1440p gaming", CategoryId = 1, NewPrice = 799, OldPrice = 899 },

                // ----- Laptops -----
                new Product { Id = 4, Name = "ASUS ROG Zephyrus G14", Description = "Compact gaming laptop with Ryzen 9 and RTX 4070", CategoryId = 2, NewPrice = 1800, OldPrice = 2000 },
                new Product { Id = 5, Name = "MacBook Pro M3", Description = "Apple’s professional laptop with M3 chip", CategoryId = 2, NewPrice = 2500, OldPrice = 2700 },
                new Product { Id = 6, Name = "Dell XPS 15", Description = "Premium ultrabook for creators and professionals", CategoryId = 2, NewPrice = 1900, OldPrice = 2100 },

                // ----- Headphones -----
                new Product { Id = 7, Name = "Sony WH-1000XM5", Description = "Noise-cancelling wireless headphones", CategoryId = 3, NewPrice = 350, OldPrice = 400 },
                new Product { Id = 8, Name = "HyperX Cloud Alpha", Description = "Gaming headset with dual chamber drivers", CategoryId = 3, NewPrice = 120, OldPrice = 150 },
                new Product { Id = 9, Name = "Apple AirPods Pro 2", Description = "Wireless earbuds with active noise cancellation", CategoryId = 3, NewPrice = 250, OldPrice = 280 },

                // ----- Monitors -----
                new Product { Id = 10, Name = "Samsung Odyssey G9", Description = "Ultra-wide curved gaming monitor 49-inch", CategoryId = 4, NewPrice = 1400, OldPrice = 1600 },
                new Product { Id = 11, Name = "LG Ultragear 27GP950", Description = "4K 144Hz gaming monitor with HDR support", CategoryId = 4, NewPrice = 800, OldPrice = 950 },
                new Product { Id = 12, Name = "Dell UltraSharp U2723QE", Description = "27-inch 4K productivity monitor with USB-C", CategoryId = 4, NewPrice = 650, OldPrice = 700 },

                // ----- Sports Equipment -----
                new Product { Id = 13, Name = "Nike Air Zoom Pegasus 40", Description = "Lightweight running shoes for all distances", CategoryId = 5, NewPrice = 130, OldPrice = 160 },
                new Product { Id = 14, Name = "Adidas Predator Accuracy", Description = "Professional football boots for firm ground", CategoryId = 5, NewPrice = 150, OldPrice = 180 },
                new Product { Id = 15, Name = "Wilson Evolution Basketball", Description = "Official indoor basketball for training and matches", CategoryId = 5, NewPrice = 60, OldPrice = 75 },
                new Product { Id = 16, Name = "Yonex Astrox 100ZZ", Description = "High-end badminton racket used by pros", CategoryId = 5, NewPrice = 220, OldPrice = 260 }
            );
        }
    }
}
