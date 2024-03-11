﻿using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<BaseResult<UserProfileViewModel>> GetUserProfile(string userName);

        Task<BaseResult> UpdateUserInfo(UserProfileViewModel viewModel);
    }
}
