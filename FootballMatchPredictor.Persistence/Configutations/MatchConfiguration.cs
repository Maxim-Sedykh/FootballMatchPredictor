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
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasData(new List<Match>()
            {
                new()
                {
                    Id = 1,
                    Team1Id = 1,
                    Team2Id = 2,
                    Team1GoalsCount = 4,
                    Team2GoalsCount = 2,
                    MatchState = MatchState.FirstTeamWon,
                    MatchDate = DateTime.SpecifyKind(new DateTime(2023, 5, 5), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Team1Id = 1,
                    Team2Id = 4,
                    Team1GoalsCount = 3,
                    Team2GoalsCount = 3,
                    MatchState = MatchState.Draw,
                    MatchDate = DateTime.SpecifyKind(new DateTime(2024, 2, 25), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    Team1Id = 1,
                    Team2Id = 3,
                    MatchState = MatchState.NotPlayedYet,
                    MatchDate = DateTime.SpecifyKind(new DateTime(2025, 5, 5), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 4,
                    Team1Id = 2,
                    Team2Id = 4,
                    MatchState = MatchState.NotPlayedYet,
                    MatchDate = DateTime.SpecifyKind(new DateTime(2025, 2, 25), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 5,
                    Team1Id = 1,
                    Team2Id = 2,
                    MatchState = MatchState.InProgress,
                    MatchDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 6,
                    Team1Id = 2,
                    Team2Id = 3,
                    MatchState = MatchState.InProgress,
                    MatchDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Coefficients)
                .WithOne(x => x.Match)
                .HasForeignKey(x => x.MatchId);
        }
    }
}
