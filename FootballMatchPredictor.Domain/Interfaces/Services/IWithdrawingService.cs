using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface IWithdrawingService
    {
        /// <summary>
        /// Вывод денег
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult> WithdrawingMoney(WithdrawingMoneyViewModel viewModel, string userName);

        /// <summary>
        /// Получение всех денежных выводов
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<WithdrawingViewModel>> GetAllWithdrawings();

        /// <summary>
        /// Получение платёжных методов для вывода денег
        /// </summary>
        /// <returns></returns>
        CollectionResult<KeyValuePair<int, string>> GetPaymentMethodsToWithdraw();

        /// <summary>
        /// Получение суммы выигрышей пользователя, для модального окна для вывода средств
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult<WithdrawingMoneyViewModel>> GetUserWinningSum(string userName);
    }
}
