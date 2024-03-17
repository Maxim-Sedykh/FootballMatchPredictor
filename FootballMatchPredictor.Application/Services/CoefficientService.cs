using FootballMatchPredictor.Domain.Entities;
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

        public async Task<CollectionResult<CoefficientViewModel>> GetMatchCoefficients(long matchId)
        {
            var coefficients = await _coefficientRepository.GetAll()
                .Include(x => x.Match.Team1)
                .Include(x => x.Match.Team2)
                .Include(x => x.CoefficientRefer)
                .Where(x => x.MatchId == matchId)
                .Select(x => x.Adapt<CoefficientViewModel>()).ToListAsync();

            return new CollectionResult<CoefficientViewModel>()
            {
                Data = coefficients
            };
        }
    }
}
