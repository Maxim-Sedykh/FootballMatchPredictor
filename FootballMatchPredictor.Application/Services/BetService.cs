using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class BetService : IBetService
    {
        private readonly IBaseRepository<Coefficient> _coefficientRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;
        private readonly IBaseRepository<Withdrawing> _withdrawingRepository;

        public BetService(IBaseRepository<Coefficient> coefficientRepository, IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository, IBaseRepository<Withdrawing> withdrawingRepository)
        {
            _coefficientRepository = coefficientRepository;
            _userRepository = userRepository;
            _betRepository = betRepository;
            _withdrawingRepository = withdrawingRepository;
        }

        public CollectionResult<KeyValuePair<int, string>> GetPaymentMethods()
        {
            var paymentMethod = Enum.GetValues(typeof(PaymentMethod))
                    .Cast<PaymentMethod>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = paymentMethod,
                Count = paymentMethod.Count
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult<MakeBetViewModel>> GetCoefficientToMakeBet(long id)
        {
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
                Data = new MakeBetViewModel(coefficient.Id, default, default)
            };
        }

        public async Task<CollectionResult<BetViewModel>> GetUserBets(string userName)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                return new CollectionResult<BetViewModel>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)StatusCode.UserNotFound,
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

            return new CollectionResult<BetViewModel>()
            {
                Data = userBets,
                Count = userBets.Count()
            };
        }

        /// <inheritdoc/>
        public async Task<BaseResult> MakeBet(MakeBetViewModel viewModel, string userName)
        {
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
                MatchId = coefficient.MatchId,
                WinningAmount = viewModel.MoneyAmount * (decimal)coefficient.CoefficientValue,
                BetTypeId = coefficient.CoefficientRefer.BetTypeId,
                CreatedAt = DateTime.UtcNow,
                BetState = BetState.Unknown
            };

            await _betRepository.CreateAsync(bet);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.BetCreated
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
                paymentMethod = (PaymentMethod)Convert.ToInt32(viewModel.PaymentMethod),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            };

            user.WinningSum -= viewModel.OutputAmount;

            await _withdrawingRepository.CreateAsync(withDrawing);
            await _userRepository.UpdateAsync(user);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.FundsWithdrawn,
            };
        }
    }
}
