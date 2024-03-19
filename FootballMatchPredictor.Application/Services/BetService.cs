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
using Mapster;
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
                Data = coefficient.Adapt<MakeBetViewModel>()
                //new MakeBetViewModel(coefficient.Id, default, default)
            };
        }

        /// <inheritdoc/>
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
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .Include(x => x.Coefficient.CoefficientRefer)
                .Include(x => x.BetType)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Adapt<BetViewModel>())
                .ToListAsync();

            return new CollectionResult<BetViewModel>()
            {
                Data = userBets,
                Count = userBets.Count
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

            if ((PaymentMethod)Convert.ToInt32(viewModel.PaymentMethod) == PaymentMethod.UserWinningAmount)
            {
                if (viewModel.MoneyAmount > user.WinningSum)
                {
                    return new BaseResult<MakeBetViewModel>()
                    {
                        ErrorMessage = ErrorMessage.InsufficientFunds,
                        ErrorCode = (int)StatusCode.InsufficientFunds,
                    };
                }

                user.WinningSum -= viewModel.MoneyAmount;
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
                Data = user.Adapt<WithdrawingMoneyViewModel>()
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

        public CollectionResult<KeyValuePair<int, string>> GetPaymentMethods(bool withUserWinnigSum)
        {
            var paymentMethods = Enum.GetValues(typeof(PaymentMethod))
                    .Cast<PaymentMethod>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            if (!withUserWinnigSum)
            {
                paymentMethods.Remove((int)PaymentMethod.UserWinningAmount);
            }

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = paymentMethods,
                Count = paymentMethods.Count
            };
        }

        public CollectionResult<KeyValuePair<int, string>> GetPaymentMethodsToBet()
        {
            var paymentMethodsToBet = GetPaymentMethods();
            
            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = paymentMethodsToBet,
                Count = paymentMethodsToBet.Count
            };
        }

        public CollectionResult<KeyValuePair<int, string>> GetPaymentMethodsToWithdraw()
        {
            var paymentMethodsToWithdraw = GetPaymentMethods();

            paymentMethodsToWithdraw.Remove((int)PaymentMethod.UserWinningAmount);


            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = paymentMethodsToWithdraw,
                Count = paymentMethodsToWithdraw.Count
            };
        }
    
        private Dictionary<int, string> GetPaymentMethods()
        {
            return Enum.GetValues(typeof(PaymentMethod))
                    .Cast<PaymentMethod>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());
        }
    }
}
