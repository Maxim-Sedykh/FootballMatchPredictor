using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class BetType: IAuditable
    {
        /// <summary>
        /// Идентификатор типа ставки
        /// </summary>
        public Guid Id { get; set; }

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

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
