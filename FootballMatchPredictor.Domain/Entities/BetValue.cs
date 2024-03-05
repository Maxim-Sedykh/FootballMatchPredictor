﻿using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class BetValue: IAuditable, IEntityId<long>
    {
        /// <summary>
        /// Идентификатор значения ставки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер значения ставки
        /// </summary>
        public byte ValueNumber { get; set; }

        /// <summary>
        /// Внешний ключ для связи со ставкой
        /// </summary>
        public long BetId { get; set; }

        /// <summary>
        /// Ставка
        /// </summary>
        public Bet Bet { get; set; }

        /// <summary>
        /// Внешний ключ для связи с информацией об значении ставки
        /// </summary>
        public long BetValueInfoId { get; set; }

        /// <summary>
        /// Информация о значении ставки
        /// </summary>
        public BetValueInfo BetValueInfo { get; set; }

        /// <summary>
        /// Значение ставки
        /// </summary>
        public float Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
