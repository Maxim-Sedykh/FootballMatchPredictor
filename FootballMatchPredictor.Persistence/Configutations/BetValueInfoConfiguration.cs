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
    public class BetValueInfoConfiguration : IEntityTypeConfiguration<BetValueInfo>
    {
        public void Configure(EntityTypeBuilder<BetValueInfo> builder)
        {
            builder.HasData(new List<BetValueInfo>()
            {
                new()
                {
                    Id = 1,
                    ValueNumber = 1,
                    ValueDescription = "Количество голов второй команды",
                    BetTypeId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 2,
                    ValueNumber = 2,
                    ValueDescription = "Количество голов первой команды",
                    BetTypeId = 1,
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id = 3,
                    ValueNumber = 1,
                    ValueDescription = "Какая команда забьёт первый гол (первая - 1, вторая - 2)",
                    BetTypeId = 2,
                    CreatedAt = DateTime.UtcNow,
                }
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ValueDescription).IsRequired().HasMaxLength(200);

            builder.HasMany(x => x.BetValues)
                .WithOne(x => x.BetValueInfo)
                .HasForeignKey(x => x.BetValueInfoId);
        }
    }
}
