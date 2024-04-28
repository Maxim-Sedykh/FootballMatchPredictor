using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.User;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResult> DeleteUser(long id)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound
                };
            }

            await _userRepository.RemoveAsync(user);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.UserDeleted
            };
        }

        public async Task<CollectionResult<UserViewModel>> GetAllUsers()
        {
            var users = await _userRepository.GetAll()
                .ToListAsync();

            var userViewModels = users
                .Select(x => x.Adapt<UserViewModel>())
                .OrderBy(x => x.Id)
                .ToList();

            return new CollectionResult<UserViewModel>()
            {
                Data = userViewModels,
                Count = userViewModels.Count
            };
        }
    }
}
