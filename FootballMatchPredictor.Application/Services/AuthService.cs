using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Helpers;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private const decimal PROMOTION_AMOUNT = 1000;

        public AuthService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<BaseResult<ClaimsIdentity>> Login(LoginViewModel viewModel)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == viewModel.Email);

            if (user == null)
            {
                return new BaseResult<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)StatusCode.UserNotFound,
                };
            }

            if (!IsVerifyPassword(user.Password, viewModel.Password))
            {
                return new BaseResult<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)StatusCode.PasswordIsWrong,
                };
            }

            var result = Authenticate(user);

            return new BaseResult<ClaimsIdentity>()
            {
                SuccessMessage = SuccessMessage.LoginCompleted,
                Data = result,
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult<ClaimsIdentity>> Register(RegisterUserViewModel viewModel)
        {
            if (viewModel.Password != viewModel.PasswordConfirm)
            {
                return new BaseResult<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)StatusCode.PasswordNotEqualsPasswordConfirm,
                };
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == viewModel.Username || x.Email == viewModel.Email);
            if (user != null)
            {
                return new BaseResult<ClaimsIdentity>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExist,
                    ErrorCode = (int)StatusCode.UserAlreadyExist,
                };
            }

            user = viewModel.Adapt<User>();

            await _userRepository.CreateAsync(user);

            var result = Authenticate(user);

            return new BaseResult<ClaimsIdentity>()
            {
                SuccessMessage = SuccessMessage.RegistrationCompleted,
                Data = result,
            };
        }

        private bool IsVerifyPassword(string userPasswordHash, string userPassword)
        {
            var hash = HashPasswordHelper.HashPassword(userPassword);
            return userPasswordHash == hash;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
