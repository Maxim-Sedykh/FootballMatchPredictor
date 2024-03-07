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

        Unauthorized = 401,

        InternalServerError = 500,
    }
}
