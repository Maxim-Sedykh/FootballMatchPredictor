using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Country;
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

        /// <summary>
        /// Получение списка стран
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<KeyValuePair<int, string>>> GetAllCountries();

        /// <summary>
        /// Создать футбольную команду
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<BaseResult> CreateTeam(CreateTeamViewModel viewModel);

        /// <summary>
        /// удалить футбольную команду
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<BaseResult> DeleteTeam(short id);

        /// <summary>
        /// Создать футбольную команду
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<TeamViewModel>> GetTeamData(short id);

        /// <summary>
        /// Создать футбольную команду
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<TeamViewModel>> UpdateTeam(UpdateTeamViewModel viewModel);
    }
}
