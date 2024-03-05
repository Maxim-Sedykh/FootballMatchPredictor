using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class CoefficientRefer : IAuditable, IEntityId<int>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание значения коэффициента
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Внешний ключ для связи с типом ставки
        /// </summary>
        public byte BetTypeId { get; set; }

        /// <summary>
        /// Тип ставки
        /// </summary>
        public BetType BetType { get; set; }

        /// <summary>
        /// Коэффициенты
        /// </summary>
        public List<Coefficient> Coefficients { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
