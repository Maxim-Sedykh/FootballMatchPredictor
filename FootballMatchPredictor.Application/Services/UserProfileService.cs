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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;

        public UserProfileService(IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository)
        {
            _userRepository = userRepository;
            _betRepository = betRepository;
        }

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
    }
}
