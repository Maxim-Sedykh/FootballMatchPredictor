using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface ICoefficientService
    {
        Task<CollectionResult<CoefficientViewModel>> GetActiveCoefficients();
    }
}
