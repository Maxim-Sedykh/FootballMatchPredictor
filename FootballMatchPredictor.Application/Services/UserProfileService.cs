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

            var userProfile = new UserProfileViewModel(user.Id, user.Username, user.FirstName, user.SurName, user.Email, user.Gender.GetDisplayName());

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
                .Where(x => x.UserId == user.Id && x.BetState != BetState.Unknown)
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
            user.Gender = (Gender)Convert.ToInt32(viewModel.Gender);

            await _userRepository.UpdateAsync(user);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.UserDataUpdated,
            };
        }
    }
}
