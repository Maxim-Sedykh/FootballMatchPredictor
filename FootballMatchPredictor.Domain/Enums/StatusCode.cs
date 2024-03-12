using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum StatusCode
    {
        UserNotFound = 11,
        UserAlreadyExist = 12,

        PasswordIsWrong = 21,
        PasswordNotEqualsPasswordConfirm = 22,

        TeamsAreEqual = 31,

        MatchNotFound = 41,
        MatchesNotFound = 42,

        TeamNotFound = 51,
        TeamsNotFound = 52,

        BetValuesNotFound = 61,

        CoefficientNotFound = 71,

        IncorrectAmount = 81,

        Unauthorized = 401,

        InternalServerError = 500,
    }
}
