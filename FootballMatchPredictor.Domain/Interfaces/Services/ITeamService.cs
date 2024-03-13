using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Team;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы с командами
    /// </summary>
    public interface ITeamService
    {
        /// <summary>
        /// Получение информации по командам
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<TeamViewModel>> GetAllTeams();
    }
}
