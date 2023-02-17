using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CommentDto
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
            IsSuccessed = false;
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationError)
        {
            IsSuccessed = false;
            ValidationErrors = validationError;
        }
    }
}