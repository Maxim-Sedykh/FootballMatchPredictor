using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class User: IAuditable, IEntityId<long>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Пол пользователя
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Захэшированный пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Пароль пользователя (обычный пользователь, модератор, админ)
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Ставки пользователя
        /// </summary>
        public List<Bet> Bets { get; set; }

        /// <summary>
        /// Захэшированный пароль пользователя
        /// </summary>
        public decimal WinningSum { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
