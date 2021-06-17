using Core.Enums;
using System;

namespace Core.Exceptions
{
    /// <summary>
    /// Represents errors when the requested data could not be found .
    /// </summary>
    public class NotFoundException : AppException
    {
        #region Ctor

        public NotFoundException()
           : base(ApiResultStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiResultStatusCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResultStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiResultStatusCode.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, exception, additionalData)
        {
        }

        #endregion Ctor
    }
}