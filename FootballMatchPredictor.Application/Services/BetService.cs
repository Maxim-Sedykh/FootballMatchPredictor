using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Database;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class BetService : IBetService
    {
        private readonly IBaseRepository<Coefficient> _coefficientRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Bet> _betRepository;
        private readonly IBaseRepository<Withdrawing> _withdrawingRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BetService(IBaseRepository<Coefficient> coefficientRepository, IBaseRepository<User> userRepository, IBaseRepository<Bet> betRepository, IBaseRepository<Withdrawing> withdrawingRepository, ILogger logger, IUnitOfWork unitOfWork)
        {
            _coefficientRepository = coefficientRepository;
            _userRepository = userRepository;
            _betRepository = betRepository;
            _withdrawingRepository = withdrawingRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
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
                .Include(x => x.Coefficient)
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

            var coefficient = await _coefficientRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == viewModel.CoefficientId);

            if (coefficient == null)
            {
                return new BaseResult<MakeBetViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound,
                };
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
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

                        await _userRepository.UpdateAsync(user);
                    }

                    Bet bet = new Bet()
                    {
                        UserId = user.Id,
                        CoefficientId = coefficient.Id,
                        BetAmountMoney = viewModel.MoneyAmount,
                        MatchId = coefficient.MatchId,
                        WinningAmount = viewModel.MoneyAmount * (decimal)coefficient.CoefficientValue,
                        CreatedAt = DateTime.UtcNow,
                        BetState = BetState.Unknown
                    };

                    await _betRepository.CreateAsync(bet);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.BetCreated
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
    
        private Dictionary<int, string> GetPaymentMethods()
        {
            return Enum.GetValues(typeof(PaymentMethod))
                    .Cast<PaymentMethod>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());
        }

        public async Task<CollectionResult<BetViewModel>> GetAllBets()
        {
            var userBets = await _betRepository.GetAll()
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .Include(x => x.Coefficient)
                .Select(x => x.Adapt<BetViewModel>())
                .ToArrayAsync();

            var userBetViewModels = userBets
                .Select(x => x.Adapt<BetViewModel>())
                .OrderBy(x => x.Id)
                .ToList();

            return new CollectionResult<BetViewModel>()
            {
                Data = userBets,
                Count = userBets.Length
            };
        }

        public CollectionResult<KeyValuePair<int, string>> GetBetTypes()
        {
            var betTypes = Enum.GetValues(typeof(BetType))
                    .Cast<BetType>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = betTypes,
                Count = betTypes.Count
            };
        }
    }
}
