using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Bet: IAuditable
    {
        /// <summary>
        /// Идентификатор ставки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Внешний ключ для связи с матчем
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Матч
        /// </summary>
        public Match Match { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом ставки
        /// </summary>
        public Guid BetTypeId { get; set; }
        
        /// <summary>
        /// Тип ставки
        /// </summary>
        public BetType BetType { get; set; }

        /// <summary>
        /// Количество поставленных денег
        /// </summary>
        public decimal BetAmountMoney { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
