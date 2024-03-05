using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces
{
    public interface IAuditable
    {
        /// <summary>
        /// Создана запись
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Последний раз обновлена запись
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
