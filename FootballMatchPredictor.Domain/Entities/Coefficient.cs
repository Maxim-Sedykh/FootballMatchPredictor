using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Coefficient: IAuditable, IEntityId<long>
    {
        /// <summary>
        /// Идентификатор коэффициента
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с матчем
        /// </summary>
        public long MatchId { get; set; }

        /// <summary>
        /// Матч данного коэффициента
        /// </summary>
        public Match Match { get; set; }

        /// <summary>
        /// значение коэффициента
        /// </summary>
        public float CoefficientValue { get; set; }

        /// <summary>
        /// Внешний ключ для коэффициента
        /// </summary>
        public int CoefficientReferId { get; set; }

        /// <summary>
        /// Активный ли коэффициент, если коэффициент активный, значит на него ещё можно поставить
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// К чему относится данный коэффициент
        /// </summary>
        public CoefficientRefer CoefficientRefer { get; set; }

        /// <summary>
        /// Ставки
        /// </summary>
        public List<Bet> Bets { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
