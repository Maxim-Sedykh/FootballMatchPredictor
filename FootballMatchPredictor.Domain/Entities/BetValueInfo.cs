﻿using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class BetValueInfo: IAuditable, IEntityId<long>
    {
        /// <summary>
        /// Идентификатор информации о значении ставки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер значения для типа ставки
        /// </summary>
        public byte ValueNumber { get; set; }

        /// <summary>
        /// Описание значения ставки
        /// </summary>
        public string ValueDescription { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом ставки
        /// </summary>
        public byte BetTypeId { get; set; }

        /// <summary>
        /// Тип ставки
        /// </summary>
        public BetType BetType { get; set; }

        /// <summary>
        /// Ставки
        /// </summary>
        public List<BetValue> BetValues { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
