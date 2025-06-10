using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Patterns
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public static ServiceResult Success(string message = null) =>
            new ServiceResult { IsSuccess = true, Message = message };

        public static ServiceResult Failure(string errorMessage) =>
            new ServiceResult { IsSuccess = false, ErrorMessage = errorMessage };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public static ServiceResult<T> Success(T data, string message = null) =>
            new ServiceResult<T> { IsSuccess = true, Data = data, Message = message };

        public static new ServiceResult<T> Failure(string errorMessage) =>
            new ServiceResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
