using Core.Enums;
using System;

namespace Core.Exceptions
{
    /// <summary>
    /// Represents errors when exception occured about business logic.
    /// </summary>
    public class LogicException : AppException
    {
        #region Ctor

        public LogicException()
    : base(ApiResultStatusCode.LogicError)
        {
        }

        public LogicException(string message)
            : base(ApiResultStatusCode.LogicError, message)
        {
        }

        public LogicException(object additionalData)
            : base(ApiResultStatusCode.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData)
            : base(ApiResultStatusCode.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ApiResultStatusCode.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.LogicError, message, exception, additionalData)
        {
        }

        #endregion Ctor
    }
}