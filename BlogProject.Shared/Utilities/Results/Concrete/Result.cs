using BlogProject.Shared.Utilities.Results.Abstract;
using BlogProject.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
             ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus,string message) : this(resultStatus)
        {
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception) : this(resultStatus, message)
        {
            Exception = exception;
        }
        
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
