using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class BetCoefficient : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new List<Bet>()
            {
                new()
                {
                    Id = 1,
                    UserId = 1,
                    CoefficientId  = 2,
                    BetAmountMoney = 10000,
                    BetTypeId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    UserId = 2,
                    CoefficientId  = 1,
                    BetAmountMoney = 10000,
                    BetTypeId = 2,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.BetValues)
                .WithOne(x => x.Bet)
                .HasForeignKey(x => x.BetId);
        }
    }
}
