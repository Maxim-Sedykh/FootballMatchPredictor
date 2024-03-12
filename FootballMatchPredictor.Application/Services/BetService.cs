using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    public class BetService : IBetService
    {
        private readonly IBaseRepository<Coefficient> _coefficientRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;

        public BetService(IBaseRepository<Coefficient> coefficientRepository, IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository)
        {
            _coefficientRepository = coefficientRepository;
            _userRepository = userRepository;
            _betRepository = betRepository;
        }

        public async Task<BaseResult<MakeBetViewModel>> GetDataToMakeBet(long id)
        {
            var paymentMethods = ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            var coefficient = await _coefficientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (coefficient == null)
            {
                return new BaseResult<MakeBetViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound,
                };
            }
            
            return new BaseResult<MakeBetViewModel>()
            {
                Data = new MakeBetViewModel(paymentMethods, coefficient.Id, default, default)
            };
        }

        public async Task<BaseResult> MakeBet(MakeBetViewModel viewModel, string userName)
        {
            if (viewModel.MoneyAmount < 1000m)
            {
                return new BaseResult<MakeBetViewModel>()
                {
                    ErrorMessage = ErrorMessage.IncorrectAmount,
                    ErrorCode = (int)StatusCode.IncorrectAmount,
                };
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new BaseResult<MakeBetViewModel>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)StatusCode.UserNotFound,
                };
            }

            var coefficient = await _coefficientRepository.GetAll()
                .Include(x => x.CoefficientRefer)
                .ThenInclude(x => x.BetType)
                .FirstOrDefaultAsync(x => x.Id == viewModel.CoefficientId);

            if (coefficient == null)
            {
                return new BaseResult<MakeBetViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound,
                };
            }

            Bet bet = new Bet()
            {
                UserId = user.Id,
                CoefficientId = coefficient.Id,
                BetAmountMoney = viewModel.MoneyAmount,
                WinningAmount = viewModel.MoneyAmount * (decimal)coefficient.CoefficientValue,
                BetTypeId = coefficient.CoefficientRefer.BetType.Id,
                CreatedAt = DateTime.UtcNow,
                BetState = BetState.Unknown
            };

            await _betRepository.CreateAsync(bet);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.BetCreated
            };
        }
    }
}
