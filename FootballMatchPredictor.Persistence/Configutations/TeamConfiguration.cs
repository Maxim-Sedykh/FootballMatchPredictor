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
                    CountryId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Name = "ЦСКА",
                    CountryId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    Name = "Зенит",
                    CountryId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 4,
                    Name = "Манчестер Юнайтед",
                    CountryId = 2,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Team1Matches)
                .WithOne(x => x.Team1)
                .HasForeignKey(x => x.Team1Id);

            builder.HasMany(x => x.Team2Matches)
                .WithOne(x => x.Team2)
                .HasForeignKey(x => x.Team2Id);
        }
    }
}
