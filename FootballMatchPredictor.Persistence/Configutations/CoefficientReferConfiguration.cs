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
    public class CoefficientReferConfiguration : IEntityTypeConfiguration<CoefficientRefer>
    {
        public void Configure(EntityTypeBuilder<CoefficientRefer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new List<CoefficientRefer>()
            {
                new()
                {
                    Id = 1,
                    Description = "Коэффициент на определённый счёт 1:1",
                    BetTypeId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    Description = "Коэффициент на то, что первая команда забьёт первый гол",
                    BetTypeId = 2,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    Description = "Коэффициент на то, что вторая команда забьёт первый гол",
                    BetTypeId = 2,
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);

            builder.HasMany(x => x.Coefficients)
                .WithOne(x => x.CoefficientRefer)
                .HasForeignKey(x => x.CoefficientReferId);
        }
    }
}
