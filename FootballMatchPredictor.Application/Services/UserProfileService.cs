using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
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

        public UserProfileService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
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

            return new BaseResult<UserProfileViewModel>()
            {
                Data = user.Adapt<UserProfileViewModel>(),
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
