using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    /// <summary>
    /// Сервис для работы с коэффициентами
    /// </summary>
    public class CoefficientService : ICoefficientService
    {
        private readonly IBaseRepository<Coefficient> _coefficientRepository;

        public CoefficientService(IBaseRepository<Coefficient> coefficientRepository)
        {
            _coefficientRepository = coefficientRepository;
        }

        public async Task<BaseResult> DeleteCoefficientById(long id)
        {
            var coefficient = await _coefficientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (coefficient == null)
            {
                return new CollectionResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound
                };
            }

            await _coefficientRepository.RemoveAsync(coefficient);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.CoefficientDeleted
            };
        }

        public async Task<CollectionResult<CoefficientViewModel>> GetAllCoefficients()
        {
            var coefficients = await _coefficientRepository.GetAll()
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .ToListAsync();

            var coefficientViewModels = coefficients
                .Select(x => x.Adapt<CoefficientViewModel>())
                .OrderBy(x => x.Id)
                .ToList();

            if (coefficientViewModels == null)
            {
                return new CollectionResult<CoefficientViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientsNotFound,
                    ErrorCode = (int)StatusCode.CoefficientsNotFound
                };
            }

            return new CollectionResult<CoefficientViewModel>()
            {
                Data = coefficientViewModels,
                Count = coefficientViewModels.Count
            };
        }

        public async Task<BaseResult<UpdateCoefficientViewModel>> GetCoefficientById(long id)
        {
            var coefficient = await _coefficientRepository.GetAll()
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (coefficient == null)
            {
                return new BaseResult<UpdateCoefficientViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound
                };
            }

            return new BaseResult<UpdateCoefficientViewModel>()
            {
                Data = coefficient.Adapt<UpdateCoefficientViewModel>()
            };
        }

        public async Task<CollectionResult<CoefficientViewModel>> GetMatchCoefficients(long matchId)
        {
            var coefficients = await _coefficientRepository.GetAll()
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .Where(x => x.MatchId == matchId)
                .Select(x => x.Adapt<CoefficientViewModel>()).ToListAsync();

            return new CollectionResult<CoefficientViewModel>()
            {
                Data = coefficients
            };
        }

        public async Task<BaseResult> UpdateCoefficient(UpdateCoefficientViewModel viewModel)
        {
            var coefficient = await _coefficientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (coefficient == null)
            {
                return new BaseResult<CoefficientViewModel>()
                {
                    ErrorMessage = ErrorMessage.CoefficientNotFound,
                    ErrorCode = (int)StatusCode.CoefficientNotFound
                };
            }

            coefficient.CoefficientValue = viewModel.CoefficientValue;
            coefficient.IsActive = viewModel.IsActive;
            coefficient.BetType = (BetType)Convert.ToInt32(viewModel.BetType);

            await _coefficientRepository.UpdateAsync(coefficient);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.CoefficientUpdated
            };
        }
    }
}
