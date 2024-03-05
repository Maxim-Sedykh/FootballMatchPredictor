﻿using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Entities
{
    public class Match: IAuditable
    {
        /// <summary>
        /// Идентификатор матча
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Внешний ключ для связи с первой командой
        /// </summary>
        public Guid Team1Id { get; set; }

        /// <summary>
        /// Первая команда
        /// </summary>
        public Team Team1 { get; set; }

        /// <summary>
        /// Внешний ключ для связи со второй командой
        /// </summary>
        public Guid Team2Id { get; set; }

        /// <summary>
        /// Вторая команда
        /// </summary>
        public Team Team2 { get; set; }

        /// <summary>
        /// Количество забитых голов первой команды
        /// </summary>
        public byte Team1GoalsCount { get; set; }

        /// <summary>
        /// Количество забитых голов второй команды
        /// </summary>
        public byte Team2GoalsCount { get; set; }

        /// <summary>
        /// Состояние матча (ещё не сыигран, в процессе, ничья, первая команда выиграла, вторая команда выиграла)
        /// </summary>
        public MatchState MatchState { get; set; }

        /// <summary>
        /// Время проведения матча
        /// </summary>
        public DateTime MatchDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
