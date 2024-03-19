using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Country: IAuditable, IEntityId<byte>
    {
        public byte Id { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Команды
        /// </summary>
        public ICollection<Team> Teams { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
