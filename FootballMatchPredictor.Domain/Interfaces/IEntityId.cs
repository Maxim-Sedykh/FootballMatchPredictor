using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces
{
    public interface IEntityId<T> where T : struct
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; }
    }
}
