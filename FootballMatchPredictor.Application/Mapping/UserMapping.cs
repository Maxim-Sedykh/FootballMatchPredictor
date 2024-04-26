using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Helpers;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using FootballMatchPredictor.Domain.ViewModels.User;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            const decimal PROMOTION_AMOUNT = 1000;

            config.NewConfig<User, UserProfileViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SurName, src => src.SurName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Gender, src => src.Gender.GetDisplayName());

            config.NewConfig<RegisterUserViewModel, User>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SurName, src => src.SurName)
                .Map(dest => dest.Password, src => HashPasswordHelper.HashPassword(src.Password))
                .Map(dest => dest.Role, src => Role.User)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.Gender, src => src.Gender)
                .Map(dest => dest.WinningSum, src => PROMOTION_AMOUNT);

            config.NewConfig<User, UserViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SurName, src => src.SurName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Gender, src => src.Gender.GetDisplayName())
                .Map(dest => dest.Role, src => src.Role.GetDisplayName())
                .Map(dest => dest.WinningSum, src => src.WinningSum)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
        }
    }
}
