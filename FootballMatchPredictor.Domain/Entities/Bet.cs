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
        /// <summary>
        /// Идентификатор ставки
        /// </summary>
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
        public float CoefficientId { get; set; }

        /// <summary>
        /// Коэффициент
        /// </summary>
        public Coefficient Coefficient { get; set; }

        /// <summary>
        /// Количество поставленных денег
        /// </summary>
        public decimal BetAmountMoney { get; set; }

        /// <summary>
        /// Внешний ключ для типа связи
        /// </summary>
        public long BetTypeId { get; set; } 

        /// <summary>
        /// Тип ставки
        /// </summary>
        public BetType BetType { get; set; }

        /// <summary>
        /// Значения ставки
        /// </summary>
        public List<BetValue> BetValues { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
