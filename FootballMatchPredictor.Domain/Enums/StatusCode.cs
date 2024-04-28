using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum StatusCode
    {
        /// <summary>
        /// Статус коды для пользователя
        /// </summary>
        UserNotFound = 11,
        UserAlreadyExist = 12,

        /// <summary>
        /// Статус коды для аутентификации
        /// </summary>
        PasswordIsWrong = 21,
        PasswordNotEqualsPasswordConfirm = 22,

        /// <summary>
        /// Статус коды для работы с командами
        /// </summary>
        TeamsAreEqual = 31,
        TeamNotFound = 32,
        TeamsNotFound = 33,
        TeamAlreadyExist = 34,

        /// <summary>
        /// Статус коды для работы с матчами
        /// </summary>
        MatchNotFound = 41,
        MatchesNotFound = 42,
        MatchAlreadyExist = 43,
        MatchTeamsAreEqual = 44,

        /// <summary>
        /// Статус коды для работы с коэффициентами
        /// </summary>
        CoefficientNotFound = 71,
        CoefficientsNotFound = 72,

        /// <summary>
        /// Статус коды для работы с денежными транзакциями
        /// </summary>
        IncorrectAmount = 81,
        InsufficientFunds = 82,

        /// <summary>
        /// Статус коды для работы со странами
        /// </summary>
        CountriesNotFound = 91,

        /// <summary>
        /// Статус коды для редких ситуаций
        /// </summary>
        Unauthorized = 401,
        InternalServerError = 500,
    }
}
