using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Database;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    internal class WithdrawingService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Withdrawing> _withdrawingRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawingService(IBaseRepository<User> userRepository, IBaseRepository<Withdrawing> withdrawingRepository, ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _withdrawingRepository = withdrawingRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CollectionResult<WithdrawingViewModel>> GetAllWithdrawings()
        {
            var withdrawings = await _withdrawingRepository.GetAll()
                .Include(x => x.User)
                .Select(x => x.Adapt<WithdrawingViewModel>())
                .ToArrayAsync();

            return new CollectionResult<WithdrawingViewModel>()
            {
                Data = withdrawings,
                Count = withdrawings.Length
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

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var withDrawing = new Withdrawing()
                    {
                        OutputAmount = viewModel.OutputAmount,
                        PaymentMethod = (PaymentMethod)Convert.ToInt32(viewModel.PaymentMethod),
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                    };

                    user.WinningSum -= viewModel.OutputAmount;

                    await _withdrawingRepository.CreateAsync(withDrawing);
                    await _userRepository.UpdateAsync(user);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.FundsWithdrawn,
            };
        }

        public CollectionResult<KeyValuePair<int, string>> GetPaymentMethodsToWithdraw()
        {
            var paymentMethodsToWithdraw = Enum.GetValues(typeof(PaymentMethod))
                    .Cast<PaymentMethod>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            paymentMethodsToWithdraw.Remove((int)PaymentMethod.UserWinningAmount);


            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = paymentMethodsToWithdraw,
                Count = paymentMethodsToWithdraw.Count
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
    }
}
