using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;
        private readonly IBaseRepository<Withdrawing> _withdrawingRepository;

        public UserProfileService(IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository,
            IBaseRepository<Withdrawing> withdrawingRepository)
        {
            _userRepository = userRepository;
            _betRepository = betRepository;
            _withdrawingRepository = withdrawingRepository;
        }

        /// <inheritdoc/>
        public async Task<BaseResult<UserProfileViewModel>> GetUserProfile(string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new BaseResult<UserProfileViewModel>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }

            var userBets = await _betRepository.GetAll()
                .Include(x => x.Coefficient)
                .ThenInclude(x => x.Match)
                .ThenInclude(x => x.Team1)
                .Include(x => x.Coefficient)
                .ThenInclude(x => x.Match)
                .ThenInclude(x => x.Team2)
                .Include(x => x.Coefficient)
                .ThenInclude(x => x.CoefficientRefer)
                .Include(x => x.BetType)
                .Where(x => x.UserId == user.Id)
                .Select(x => new BetViewModel(x.Id, x.Coefficient.Match.Team1.Name,
                x.Coefficient.Match.Team1.Name, x.Coefficient.CoefficientValue,
                x.Coefficient.CoefficientRefer.Description, x.BetAmountMoney, x.WinningAmount, x.BetType.TypeName, x.BetState.GetDisplayName(), x.CreatedAt))
                .ToListAsync();

            var userProfile = user.Adapt<UserProfileViewModel>() with { UserBets = userBets };

            return new BaseResult<UserProfileViewModel>()
            {
                Data = userProfile,
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult<UserStatisticsViewModel>> GetUserStatistics(string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new BaseResult<UserStatisticsViewModel>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }

            var bets = await _betRepository.GetAll()
                .Where(x => x.UserId == user.Id || x.BetState != BetState.Unknown)
                .ToListAsync();

            var betsCount = bets.Count;

            var winningBetsCount = bets.Where(x => x.BetState == BetState.Winning).ToList().Count;

            var winRate = (float)winningBetsCount / betsCount * 100;

            return new BaseResult<UserStatisticsViewModel>()
            {
                Data = new UserStatisticsViewModel(user.WinningSum, betsCount, winRate),
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult<WithdrawingMoneyViewModel>> GetUserWinningSum(string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new BaseResult<WithdrawingMoneyViewModel>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }

            return new BaseResult<WithdrawingMoneyViewModel>()
            {
                Data = new WithdrawingMoneyViewModel(default, default, user.WinningSum)
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult> UpdateUserInfo(UserProfileViewModel viewModel)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (user == null)
            {
                return new BaseResult<UserProfileViewModel>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }

            user.FirstName = viewModel.FirstName;
            user.SurName = viewModel.SurName;
            user.Email = viewModel.Email;
            user.Sex = viewModel.Sex;

            await _userRepository.UpdateAsync(user);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.UserDataUpdated,
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult> WithdrawingMoney(WithdrawingMoneyViewModel viewModel, string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new BaseResult<WithdrawingMoneyViewModel>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound,
                };
            }

            if (user.WinningSum < viewModel.OutputAmount)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.InsufficientFunds,
                    ErrorMessage = ErrorMessage.InsufficientFunds,
                };
            }

            var withDrawing = new Withdrawing()
            {
                OutputAmount = viewModel.OutputAmount,
                paymentMethod = viewModel.PaymentMethod,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            };

            await _withdrawingRepository.CreateAsync(withDrawing);

            user.WinningSum -= viewModel.OutputAmount;

            await _userRepository.UpdateAsync(user);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.FundsWithdrawn,
            };
        }
    }
}
