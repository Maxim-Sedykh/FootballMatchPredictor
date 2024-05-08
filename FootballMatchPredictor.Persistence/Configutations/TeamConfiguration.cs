using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasData(new List<Team>()
            {
                new()
                {
                    Id = 1,
                    Name = "Краснодар",
                    MatchesPlayed = 2,
                    MatchesWon = 1,
                    Rating = 1650,
                    Country = "Russia - Russian Federation",
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Name = "ЦСКА",
                    MatchesPlayed = 2,
                    MatchesWon = 0,
                    Rating = 1760,
                    Country = "Russia - Russian Federation",
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    Name = "Зенит",
                    MatchesPlayed = 0,
                    MatchesWon = 0,
                    Rating = 1500,
                    Country = "Russia - Russian Federation",
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 4,
                    Name = "Манчестер Юнайтед",
                    MatchesPlayed = 1,
                    MatchesWon = 0,
                    Rating = 1970,
                    Country = "United Kingdom - United Kingdom of Great Britain and Northern Ireland",
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Rating)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasDefaultValue(1000);

            builder.HasMany(x => x.Team1Matches)
                .WithOne(x => x.Team1)
                .HasForeignKey(x => x.Team1Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Team2Matches)
                .WithOne(x => x.Team2)
                .HasForeignKey(x => x.Team2Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
