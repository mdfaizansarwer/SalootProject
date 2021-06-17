using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SalootProject.Api.Midllewares
{
    public class CustomExceptionHandlerMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        #endregion Fields

        #region Ctor

        public CustomExceptionHandlerMiddleware(RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        #endregion Ctor

        #region Methods

        public async Task Invoke(HttpContext context)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            try
            {
                await _next(context);
            }
            catch (AppException exception)
            {
                Log.Error(exception, $"Middleware ---> AppException : {exception.Message}");

                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }
                    if (exception.AdditionalData != null)
                    {
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }
                await WriteToResponseAsync();
            }
            catch (SecurityTokenExpiredException exception)
            {
                Log.Error(exception, $"Middleware ---> SecurityTokenExpiredException : {exception.Message}");

                SetUnAuthorizeResponse(exception, ApiResultStatusCode.ExpiredSecurityToken);
                await WriteToResponseAsync();
            }
            catch (UnauthorizedAccessException exception)
            {
                Log.Error(exception, $"Middleware ---> UnauthorizedAccessException : {exception.Message}");

                SetUnAuthorizeResponse(exception, ApiResultStatusCode.UnAuthorized);
                await WriteToResponseAsync();
            }
            catch (Exception exception)
            {

                Log.Error(exception, $"Middleware ---> Exception : {exception.Message}");

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    message = JsonConvert.SerializeObject(dic, jsonSerializerSettings);
                }
                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                if (context.Response.HasStarted)
                {
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");
                }

                var result = new ApiResponse(false, apiStatusCode, message);
                var json = JsonConvert.SerializeObject(result, jsonSerializerSettings);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }

            void SetUnAuthorizeResponse(Exception exception, ApiResultStatusCode apiResultStatusCode)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = apiResultStatusCode;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    if (exception is SecurityTokenExpiredException tokenException)
                    {
                        message = JsonConvert.SerializeObject(dic, jsonSerializerSettings);
                    }
                }
            }
        }

        #endregion Methods
    }
}