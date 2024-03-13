using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Withdrawing : IAuditable, IEntityId<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// Количество выводимых средств
        /// </summary>
        public decimal OutputAmount { get; set; }

        /// <summary>
        /// Платёжный способ
        /// </summary>
        public PaymentMethod paymentMethod { get; set; }

        /// <summary>
        /// Внешний ключ для связи с пользователем
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
