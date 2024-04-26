using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Bet: IAuditable, IEntityId<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Внешний ключ для связи с коэффициентами
        /// </summary>
        public long CoefficientId { get; set; }

        /// <summary>
        /// Коэффициент
        /// </summary>
        public Coefficient Coefficient { get; set; }

        /// <summary>
        /// Внешний ключ для связи с матчами
        /// </summary>
        public long MatchId { get; set; }

        /// <summary>
        /// Матч
        /// </summary>
        public Match Match { get; set; }

        /// <summary>
        /// Количество поставленных денег
        /// </summary>
        public decimal BetAmountMoney { get; set; }

        /// <summary>
        /// Количество поставленных денег
        /// </summary>
        public decimal WinningAmount { get; set; }

        /// <summary>
        /// Состояние ставки
        /// </summary>
        public BetState BetState { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
