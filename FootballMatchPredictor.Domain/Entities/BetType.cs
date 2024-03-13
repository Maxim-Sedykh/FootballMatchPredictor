using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class BetType: IAuditable, IEntityId<byte>
    {
        public byte Id { get; set; }

        /// <summary>
        /// Название типа ставки
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Количество необходимых значений для этого типа ставок
        /// </summary>
        public byte ValuesCount { get; set; }

        /// <summary>
        /// Количество коэффициентов для этой ставки
        /// </summary>
        public float CoefficientCount { get; set; }

        /// <summary>
        /// Информация по возможным коэффициентам
        /// </summary>
        public List<CoefficientRefer> CoefficientRefers { get; set; }

        /// <summary>
        /// Коэффициенты по этому типу ставки
        /// </summary>
        public List<Coefficient> Coefficients { get; set; }

        /// <summary>
        /// Информация по ставкам
        /// </summary>
        public List<Bet> Bets { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
