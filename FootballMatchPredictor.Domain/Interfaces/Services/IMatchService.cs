﻿using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы с матчами
    /// </summary>
    public interface IMatchService
    {
        /// <summary>
        /// Получение матчей разных типов (ещё не сыиграны, )
        /// </summary>
        /// <returns></returns>
        Task<BaseResult<MatchPageViewModel>> GetAllMatches();

        /// <summary>
        /// Получение матчей определённых команд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CollectionResult<TeamMatchViewModel>> GetTeamMatches(long id);
    }
}
