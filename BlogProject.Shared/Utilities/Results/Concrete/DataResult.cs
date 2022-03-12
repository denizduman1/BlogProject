using BlogProject.Shared.Entities.Abstract;
using BlogProject.Shared.Utilities.Results.Abstract;
using BlogProject.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>        
    {
        public DataResult(T data, ResultStatus resultStatus) : base(resultStatus)
        {
            Data = data;
        }
        public DataResult(T data, ResultStatus resultStatus, string message) : base(resultStatus, message)
        {
            Data = data;
        }
        public DataResult(T data, ResultStatus resultStatus, string message, Exception exception) : base(resultStatus, message, exception)
        {
            Data = data;
        }

        public T Data {get;}
    }
}
