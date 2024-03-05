using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Coefficient: IAuditable
    {
        /// <summary>
        /// Идентификатор коэффициента
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с матчем
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Матч данного коэффициента
        /// </summary>
        public Match Match { get; set; }

        /// <summary>
        /// значение коэффициента
        /// </summary>
        public float CoefficientValue { get; set; }

        /// <summary>
        /// К чему относится данный коэффициент
        /// </summary>
        public CoefficientRefer CoefficientRefer { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом ставки
        /// </summary>
        public Guid BetTypeId { get; set; }

        /// <summary>
        /// Тип ставки
        /// </summary>
        public BetType BetType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
