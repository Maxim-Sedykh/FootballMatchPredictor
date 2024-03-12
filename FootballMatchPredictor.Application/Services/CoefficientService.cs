using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Services
{
    public class CoefficientService : ICoefficientService
    {
        private readonly IBaseRepository<Coefficient> _coefficientRepository;

        public CoefficientService(IBaseRepository<Coefficient> coefficientRepository)
        {
            _coefficientRepository = coefficientRepository;
        }

        public async Task<CollectionResult<CoefficientViewModel>> GetActiveCoefficients()
        {
            var coefficients = await _coefficientRepository.GetAll()
                .Include(x => x.CoefficientRefer)
                .Include(x => x.Match)
                .ThenInclude(x => x.Team1)
                .Include(x => x.Match)
                .ThenInclude(x => x.Team2)
                .Where(x => x.IsActive)
                .Select(x => new CoefficientViewModel(x.Id, x.Match.Team1.Name, x.Match.Team2.Name,
                    x.CoefficientValue, x.CoefficientRefer.Description, x.CreatedAt))
                .ToListAsync();

            return new CollectionResult<CoefficientViewModel>()
            {
                Data = coefficients,
                Count = coefficients.Count
            };
        }
    }
}
