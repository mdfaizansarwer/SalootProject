using Core.Enums;
using System;

namespace Core.Exceptions
{
    /// <summary>
    /// Represents errors that occur when invalid arguments passed.
    /// </summary>
    public class BadRequestException : AppException
    {
        #region Ctor

        public BadRequestException()
    : base(ApiResultStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(ApiResultStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(ApiResultStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(ApiResultStatusCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(ApiResultStatusCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.BadRequest, message, exception, additionalData)
        {
        }

        #endregion Ctor
    }
}