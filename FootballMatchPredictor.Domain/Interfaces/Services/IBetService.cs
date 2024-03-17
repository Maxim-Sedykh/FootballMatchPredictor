using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
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
        /// Получение платёжных методов
        /// </summary>
        /// <returns></returns>
        CollectionResult<KeyValuePair<int, string>> GetPaymentMethods();

        /// <summary>
        /// Получение суммы выигрышей пользователя, для модального окна для вывода средств
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult<WithdrawingMoneyViewModel>> GetUserWinningSum(string userName);

        /// <summary>
        /// Вывод денег
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult> WithdrawingMoney(WithdrawingMoneyViewModel viewModel, string userName);
    }
}
