using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(a => a.AppUser)
                   .WithOne(u => u.Address)
                   .HasForeignKey<Address>(a => a.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Address
                {
                    Id = 1,
                    FirstName = "Erling",
                    LastName = "Haaland",
                    City = "Manchester",
                    ZipCode = "M11 3FF",
                    Street = "Etihad Stadium Road",
                    State = "England",
                    AppUserId = "29d9dcca-7ccb-4295-acf2-6f0d1f6ae281"
                },
                new Address
                {
                    Id = 2,
                    FirstName = "Mohamed",
                    LastName = "Salah",
                    City = "Liverpool",
                    ZipCode = "L1 4BX",
                    Street = "Anfield Road",
                    State = "England",
                    AppUserId = "00f772b6-25aa-4385-a3ee-99f60c0e1006"
                },
                new Address
                {
                    Id = 3,
                    FirstName = "Luka",
                    LastName = "Modric",
                    City = "Madrid",
                    ZipCode = "28022",
                    Street = "Santiago Bernabeu Ave",
                    State = "Spain",
                    AppUserId = "f824594d-9cab-433f-b93a-6b80d14c6ca3"
                },
                new Address
                {
                    Id = 4,
                    FirstName = "Robert",
                    LastName = "Lewandowski",
                    City = "Barcelona",
                    ZipCode = "08028",
                    Street = "Camp Nou Street",
                    State = "Spain",
                    AppUserId = "a6f90d83-bdff-48cd-914d-234d9a4dcde8"
                }
            );
        }
    }
}
