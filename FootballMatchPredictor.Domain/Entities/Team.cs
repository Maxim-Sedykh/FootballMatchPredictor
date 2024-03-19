using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Team: IAuditable, IEntityId<short>
    {
        public short Id { get; set; }

        /// <summary>
        /// Название команды
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Матчей выиграно
        /// </summary>
        public int MatchesPlayed { get; set; }

        /// <summary>
        /// Матчей выиграно
        /// </summary>
        public int MatchesWon { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public byte CountryId { get; set; }

        /// <summary>
        /// Страна команды
        /// </summary>
        public Country Country { get; set; }
        
        /// <summary>
        /// Матчи команды в качестве первой команды в плане нумерования
        /// </summary>
        public ICollection<Match> Team1Matches { get; set; }

        /// <summary>
        /// Матчи команды в качестве второй команды в плане нумерования
        /// </summary>
        public ICollection<Match> Team2Matches { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
