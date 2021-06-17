using Core.Enums;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SalootProject.Api.Midllewares
{
    public class ApiResponse
    {
        #region Properties

        public bool IsSuccess { get; set; }

        public ApiResultStatusCode StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        #endregion Properties

        #region Ctor

        public ApiResponse(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        #endregion Ctor

        #region Implicit Operators

        public static implicit operator ApiResponse(OkResult result)
        {
            return new ApiResponse(true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResponse(BadRequestResult result)
        {
            return new ApiResponse(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResponse(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResponse(false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResponse(ContentResult result)
        {
            return new ApiResponse(true, ApiResultStatusCode.Success, result.Content);
        }

        public static implicit operator ApiResponse(NotFoundResult result)
        {
            return new ApiResponse(false, ApiResultStatusCode.NotFound);
        }

        #endregion Implicit Operators
    }

    public class ApiResponse<TData> : ApiResponse
        where TData : class
    {
        #region Properties

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        #endregion Properties

        #region Ctor

        public ApiResponse(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        #endregion Ctor

        #region Implicit Operators

        public static implicit operator ApiResponse<TData>(TData data)
        {
            return new ApiResponse<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResponse<TData>(OkResult result)
        {
            return new ApiResponse<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResponse<TData>(OkObjectResult result)
        {
            return new ApiResponse<TData>(true, ApiResultStatusCode.Success, (TData)result.Value);
        }

        public static implicit operator ApiResponse<TData>(BadRequestResult result)
        {
            return new ApiResponse<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResponse<TData>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResponse<TData>(false, ApiResultStatusCode.BadRequest, null, message);
        }

        public static implicit operator ApiResponse<TData>(ContentResult result)
        {
            return new ApiResponse<TData>(true, ApiResultStatusCode.Success, null, result.Content);
        }

        public static implicit operator ApiResponse<TData>(NotFoundResult result)
        {
            return new ApiResponse<TData>(false, ApiResultStatusCode.NotFound, null);
        }

        public static implicit operator ApiResponse<TData>(NotFoundObjectResult result)
        {
            return new ApiResponse<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }

        #endregion Implicit Operators
    }
}