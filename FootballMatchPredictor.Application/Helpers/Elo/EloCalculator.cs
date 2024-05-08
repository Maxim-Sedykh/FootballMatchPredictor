using FootballMatchPredictor.Application.Helpers.EloClasses;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Helpers.Elo
{
    public class EloCalculator
    {
        private const double K_FACTOR = 400.0;

        public static MatchValue CalculateBettingCoefficients(float team1Rating, float team2Rating)
        {
            float expectedWinProbabilityTeam1 = CalculateExpectedWinProbability(team1Rating, team2Rating);
            float firstTeamWinCoefficient = 1 / expectedWinProbabilityTeam1;
            float secondTeamWinCoefficient = 1 - expectedWinProbabilityTeam1;
            float drawCoefficient = 1 / (1 - expectedWinProbabilityTeam1);

            return new MatchValue()
            {
                FirstTeamValue = firstTeamWinCoefficient,
                SecondTeamValue = secondTeamWinCoefficient,
                DrawValue = drawCoefficient
            };
        }

        private static float CalculateExpectedWinProbability(float team1Rating, float team2Rating)
        {
            return (float)(1 / (1 + Math.Pow(10, (team2Rating - team1Rating) / K_FACTOR)));
        }

        public static MatchValue CalculateProbabilities(float ratingTeam1, float ratingTeam2)
        {
            float expectedWinProbabilityTeam1 = CalculateExpectedWinProbability(ratingTeam1, ratingTeam2);
            float p1 = expectedWinProbabilityTeam1 * 100;
            float p2 = (1 - expectedWinProbabilityTeam1) * 100;
            float draw = 100 - p1 - p2;

            return new MatchValue()
            {
                FirstTeamValue = expectedWinProbabilityTeam1 * 100,
                SecondTeamValue = (1 - expectedWinProbabilityTeam1) * 100,
                DrawValue = 100 - p1 - p2
            };
        }
    }
}
