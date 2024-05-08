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
        /// Рейтинг команды по методу Эло, этот рейтинг показывает мастерство команды,
        /// чем он выше, тем команда лучше играет, начальный рейтинг команды - 1500,
        /// самый маленький рейтинг, который может быть - 1000, самый большой - 2500
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Матчей выиграно
        /// </summary>
        public int MatchesPlayed { get; set; }

        /// <summary>
        /// Матчей выиграно
        /// </summary>
        public int MatchesWon { get; set; }

        /// <summary>
        /// Страна команды
        /// </summary>
        public string Country { get; set; }
        
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
