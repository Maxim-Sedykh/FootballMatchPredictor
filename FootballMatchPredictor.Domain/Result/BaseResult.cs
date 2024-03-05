using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Result
{
    public class BaseResult
    {
        public bool IsSuccess => ErrorMessage == null;

        public string ErrorMessage { get; set; }

        public int? ErrorCode { get; set; }

        public string SuccessMessage { get; set; }
    }

    public class BaseResult<T> : BaseResult
    {
        public BaseResult(string errorMessage, int errorCode, T data)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Data = data;
        }

        public BaseResult(string errorMessage, int errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public BaseResult(T data)
        {
            Data = data;
        }

        public BaseResult(string successMessage)
        {
            SuccessMessage = successMessage;
        }

        public BaseResult() { }

        public T? Data { get; set; }
    }
}
