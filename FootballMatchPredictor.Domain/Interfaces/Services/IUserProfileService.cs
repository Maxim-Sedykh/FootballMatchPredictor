using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы с профилем пользователя
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Получение данных о профиле пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult<UserProfileViewModel>> GetUserProfile(string userName);

        /// <summary>
        /// Обновление информации пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<BaseResult> UpdateUserInfo(UserProfileViewModel viewModel);

        /// <summary>
        /// Получение статистики по ставкам пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult<UserStatisticsViewModel>> GetUserStatistics(string userName);

        /// <summary>
        /// Вывод денежных средств из аккаунта на любой платёжный сервис
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult> WithdrawingMoney(WithdrawingMoneyViewModel viewModel, string userName);

        /// <summary>
        /// Получение суммы выигрыша от ставок для страницы с выводом денег
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<BaseResult<WithdrawingMoneyViewModel>> GetUserWinningSum(string userName);
    }
}
