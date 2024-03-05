using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Helpers;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class CardConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Username = "SuperAdmin",
                    Email = "Admin_m@mail.ru",
                    Password = HashPasswordHelper.HashPassword("admin123"),
                    Role = Role.Admin,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Username = "StavoChnik",
                    Email = "min_mexd@mail.ru",
                    Password = HashPasswordHelper.HashPassword("12341234"),
                    Role = Role.User,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
