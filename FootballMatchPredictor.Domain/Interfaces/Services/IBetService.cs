using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы со ставками
    /// </summary>
    public interface IBetService
    {
        /// <summary>
        /// Получение информации (словаря платёжных методов) для того, чтобы сделать ставку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<MakeBetViewModel>> GetCoefficientToMakeBet(long id);

        /// <summary>
        /// Сделать ставку на определённый коэффициент
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult> MakeBet(MakeBetViewModel viewModel, string userName);

        /// <summary>
        /// Получить ставки пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<CollectionResult<BetViewModel>> GetUserBets(string userName);

        /// <summary>
        /// Получение платёжных методов для ставки
        /// </summary>
        /// <returns></returns>
        CollectionResult<KeyValuePair<int, string>> GetPaymentMethodsToBet();

        /// <summary>
        /// Получение всех ставок пользователя
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<BetViewModel>> GetAllBets();

        /// <summary>
        /// Получить типы ставок
        /// </summary>
        /// <returns></returns>
        CollectionResult<KeyValuePair<int, string>> GetBetTypes();
    }
}
