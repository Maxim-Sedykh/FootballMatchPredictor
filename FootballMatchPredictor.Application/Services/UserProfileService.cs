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
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FootballMatchPredictor.Application.Services
{
    [Authorize]
    /// <inheritdoc/>
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;

        private const decimal PROMOTION_AMOUNT = 1000;

        public UserProfileService(IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository)
        {
            _userRepository = userRepository;
            _betRepository = betRepository;
        }

        public CollectionResult<KeyValuePair<int, string>> GetGenders()
        {
            var genders = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = genders,
                Count = genders.Count
            };
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

            return new BaseResult<UserProfileViewModel>()
            {
                Data = user.Adapt<UserProfileViewModel>()

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
                .Where(x => x.UserId == user.Id && x.BetState != BetState.Unknown)
                .ToListAsync();

            var betsCount = bets.Count;
            var winRate = (float)bets.Where(x => x.BetState == BetState.Winning).ToList().Count / betsCount * 100;

            return new BaseResult<UserStatisticsViewModel>()
            {
                Data = new UserStatisticsViewModel(user.WinningSum, betsCount, winRate),
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

            if (user.FirstName != viewModel.FirstName ||
                user.SurName != viewModel.SurName ||
                user.Email != viewModel.Email ||
                user.Gender.GetDisplayName() != viewModel.Gender)
            {
                user = viewModel.Adapt(user);

                await _userRepository.UpdateAsync(user);

                return new BaseResult()
                {
                    SuccessMessage = SuccessMessage.UserDataUpdated,
                };
            }

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.NoChangesDetected,
            };
        }

        public async Task PromotionalBalanceIncrease()
        {
            var users = await _userRepository.GetAll().ToListAsync();

            users.ForEach(user => user.WinningSum += PROMOTION_AMOUNT);

            await _userRepository.UpdateRangeAsync(users);
        }
    }
}
