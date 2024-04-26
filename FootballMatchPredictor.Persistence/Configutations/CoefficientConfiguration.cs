using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FootballMatchPredictor.Domain.Enums;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class CoefficientConfiguration : IEntityTypeConfiguration<Coefficient>
    {
        public void Configure(EntityTypeBuilder<Coefficient> builder)
        {
            builder.HasData(new List<Coefficient>()
            {
                new()
                {
                    Id = 1,
                    MatchId = 1,
                    CoefficientValue = 2.4f,
                    IsActive = false,
                    BetType = BetType.SecondTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    MatchId = 1,
                    CoefficientValue = 1.7f,
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    MatchId = 2,
                    CoefficientValue = 1.4f,
                    IsActive = false,
                    BetType = BetType.FirstTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 4,
                    MatchId = 3,
                    CoefficientValue = 2.5f,
                    IsActive = true,
                    BetType = BetType.FirstTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 5,
                    MatchId = 3,
                    CoefficientValue = 1.3f,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 6,
                    MatchId = 4,
                    CoefficientValue = 1.4f,
                    IsActive = true,
                    BetType = BetType.FirstTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 7,
                    MatchId = 4,
                    CoefficientValue = 2.4f,
                    IsActive = true,
                    BetType = BetType.FirstTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 8,
                    MatchId = 4,
                    CoefficientValue = 3.4f,
                    IsActive = true,
                    BetType = BetType.SecondTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 9,
                    MatchId = 5,
                    CoefficientValue = 4.4f,
                    IsActive = false,
                    BetType = BetType.FirstTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 10,
                    MatchId = 6,
                    CoefficientValue = 1.3f,
                    IsActive = false,
                    BetType = BetType.Draw,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 11,
                    MatchId = 6,
                    CoefficientValue = 1.1f,
                    IsActive = true,
                    BetType = BetType.SecondTeamWon,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.Coefficient)
                .HasForeignKey(x => x.CoefficientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
