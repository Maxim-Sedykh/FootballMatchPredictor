using FootballMatchPredictor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class BetTypeCoefficient : IEntityTypeConfiguration<BetType>
    {
        public void Configure(EntityTypeBuilder<BetType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new List<BetType>()
            {
                new()
                {
                    Id = 1,
                    TypeName = "Точный счёт",
                    ValuesCount = 2,
                    CoefficientCount = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 1,
                    TypeName = "Ставка на первый гол",
                    ValuesCount = 1,
                    CoefficientCount = 2,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.TypeName).IsRequired().HasMaxLength(30);

            builder.HasMany(x => x.BetValueInfos)
                .WithOne(x => x.BetType)
                .HasForeignKey(x => x.BetTypeId);

            builder.HasMany(x => x.CoefficientRefers)
                .WithOne(x => x.BetType)
                .HasForeignKey(x => x.BetTypeId);

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.BetType)
                .HasForeignKey(x => x.BetTypeId);
        }
    }
}
