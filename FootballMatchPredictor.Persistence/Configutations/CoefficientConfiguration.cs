using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class CoefficientConfiguration : IEntityTypeConfiguration<Coefficient>
    {
        public void Configure(EntityTypeBuilder<Coefficient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new List<Coefficient>()
            {
                new()
                {
                    Id = 1,
                    MatchId = 1,
                    CoefficientValue = 2.4f,
                    CoefficientReferId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    MatchId = 1,
                    CoefficientValue = 1.7f,
                    CoefficientReferId = 2,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    MatchId = 2,
                    CoefficientValue = 1.4f,
                    CoefficientReferId = 3,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.Coefficient)
                .HasForeignKey(x => x.CoefficientId);
        }
    }
}
