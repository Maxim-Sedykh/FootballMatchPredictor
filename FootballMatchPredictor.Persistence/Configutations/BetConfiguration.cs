using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballMatchPredictor.Domain.Enums;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class BetCoefficient : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasData(new List<Bet>()
            {
                new()
                {
                    Id = 1,
                    UserId = 1,
                    CoefficientId  = 2,
                    BetAmountMoney = 10000,
                    WinningAmount = 17000,
                    MatchId = 1,
                    BetState = BetState.Winning,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    UserId = 1,
                    CoefficientId  = 3,
                    BetAmountMoney = 20000,
                    WinningAmount = 28000,
                    MatchId = 2,
                    BetState = BetState.Winning,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    UserId = 2,
                    CoefficientId  = 1,
                    BetAmountMoney = 10000,
                    WinningAmount = 24000,
                    MatchId = 1,
                    BetState = BetState.Losing,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
