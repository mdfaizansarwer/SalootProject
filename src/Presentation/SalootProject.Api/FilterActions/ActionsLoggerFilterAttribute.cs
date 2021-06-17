using Core.Enums;
using Data.Entities;
using Date;
using Microsoft.AspNetCore.Mvc.Filters;
using SalootProject.Api.Midllewares;
using System;
using System.Collections.Generic;

namespace SalootProject.Api.FilterActions
{
    public sealed class ActionsLoggerFilterAttribute : ActionFilterAttribute
    {
        #region Properties

        private string ClientIpAddress { get; set; }

        private string ControllerName { get; set; }

        private string ActionName { get; set; }

        private string HttpMethod { get; set; }

        private string ActionArguments { get; set; }

        private string RequestPath { get; set; }

        private ApiResultStatusCode ApiResultStatusCode { get; set; }

        private bool IsCanceled { get; set; }

        #endregion Properties

        #region Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion Fields

        #region Ctor

        public ActionsLoggerFilterAttribute(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Ctor

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.RouteValues.ContainsKey("controller"))
            {
                ControllerName = context.ActionDescriptor.RouteValues["controller"];
            }

            if (context.ActionDescriptor.RouteValues.ContainsKey("action"))
            {
                ActionName = context.ActionDescriptor.RouteValues["action"];
            }
            HttpMethod = context.HttpContext.Request.Method;
            ActionArguments = GetActionArgumentsAsString(context.ActionArguments);
            RequestPath = context.HttpContext.Request.Path;
            ClientIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            #region Local functions

            // Return a string from context.ActionArguments with their values
            string GetActionArgumentsAsString(IDictionary<string, object> dictionary)
            {
                var output = "";
                foreach (var item in dictionary)
                {
                    output += $"{item.Key} ";

                    if (item.Value.GetType().GetProperties().Length != 0)
                    {
                        var str = MergePropertiesWithValues(item.Value);
                        output += $"--> {str} |";
                    }
                    else
                    {
                        output += $": {item.Value} | ";
                    }
                }

                return output;
            }

            // Return a string from list of properties with their values with Comma-separated values
            string MergePropertiesWithValues(object obj)
            {
                var output = new List<string>();
                foreach (var item in obj.GetType().GetProperties())
                {
                    output.Add($"{item.Name} : {item.GetValue(obj)}");
                }

                return string.Join(",", output);
            }

            #endregion Local functions
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ApiResponse apiResult)
            {
                ApiResultStatusCode = apiResult.StatusCode;
            }

            IsCanceled = context.Canceled;

            SaveLog();
        }

        private void SaveLog()
        {
            var actionsLog = new ActionsLog
            {
                ClientIpAddress = ClientIpAddress,
                ControllerName = ControllerName,
                ActionName = ActionName,
                HttpMethod = HttpMethod,
                ActionArguments = ActionArguments,
                RequestPath = RequestPath,
                ApiResultStatusCode = ApiResultStatusCode,
                IsCanceled = IsCanceled,
                CreatedOn = DateTime.Now
            };

            _dbContext.Add(actionsLog);
            _dbContext.SaveChanges();
        }

        #endregion Methods
    }
}