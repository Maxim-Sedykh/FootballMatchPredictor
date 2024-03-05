using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;

namespace FootballMatchPredictor.Persistence.Configutations
{
    public class CardConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new List<User>()
            {
                new()
                {
                    Id
                    Username
                    Email
                    Password = 
                    Role = Role.Admin
                    CreatedAt = DateTime.UtcNow,
                },
                new()
                {
                    Id
                    Username
                    Email
                    Password =
                    Role = Role.Admin
                    CreatedAt = DateTime.UtcNow,
                },
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.CVV).IsRequired().HasMaxLength(4);
        }
    }
}
