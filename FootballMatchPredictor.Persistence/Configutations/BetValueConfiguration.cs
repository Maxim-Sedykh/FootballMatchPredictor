using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class BetValueConfiguration : IEntityTypeConfiguration<BetValue>
    {
        public void Configure(EntityTypeBuilder<BetValue> builder)
        {
            builder.HasData(new List<BetValue>()
            {
                new()
                {
                    Id = 1,
                    ValueNumber = 1,
                    BetId = 1,
                    BetValueInfoId = 1,
                    Value = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    ValueNumber = 2,
                    BetId = 1,
                    BetValueInfoId = 2,
                    Value = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    ValueNumber = 1,
                    BetId = 2,
                    BetValueInfoId = 3,
                    Value = 1,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
