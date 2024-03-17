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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Username = "SuperAdmin",
                    FirstName = "Admin",
                    SurName = "Admin",
                    Email = "Admin_m@mail.ru",
                    Password = HashPasswordHelper.HashPassword("admin123"),
                    Gender = Gender.Woman,
                    Role = Role.Admin,
                    WinningSum = 45000,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Username = "StavoChnik",
                    FirstName = "Ivan",
                    SurName = "Ivanov",
                    Email = "min_mexd@mail.ru",
                    Password = HashPasswordHelper.HashPassword("12341234"),
                    Gender = Gender.Man,
                    Role = Role.User,
                    WinningSum = 0,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(30);

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Withdrawings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
