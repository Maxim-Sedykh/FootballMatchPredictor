using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Данный сервис отвечает за работу с коэффициентами
    /// </summary>
    public interface ICoefficientService
    {
        /// <summary>
        /// Получение коэффициентов определённого матча (ещё не сыиграны, играются сейчас, сыиграны)
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<CoefficientViewModel>> GetMatchCoefficients(long matchId);

        /// <summary>
        /// Получение всех коэффициентов
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<CoefficientViewModel>> GetAllCoefficients();

        /// <summary>
        /// Получение данных о коэффициенте по его идентификатору
        /// </summary>
        /// <returns></returns>
        Task<BaseResult<CoefficientViewModel>> GetCoefficientById(long id);

        /// <summary>
        /// Удаление коэффициента по его идентификатору
        /// </summary>
        /// <returns></returns>
        Task<BaseResult> DeleteCoefficientById(long id);

        /// <summary>
        /// Обновление данных коэффициента
        /// </summary>
        /// <returns></returns>
        Task<BaseResult> UpdateCoefficient(UpdateCoefficientViewModel viewModel);
    }
}
