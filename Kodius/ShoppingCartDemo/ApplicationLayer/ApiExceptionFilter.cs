using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShoppingCartDemo.BusinessLayer;
using ShoppingCartDemo.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.ApplicationLayer
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        private void HandleApiException(ExceptionContext context)
        {
            ApiError apiError = null;
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                apiError.errors = ex.Errors;

                context.HttpContext.Response.StatusCode = ex.StatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
            }
            else if (context.Exception is EntityNotFoundException)
            {
                var ex = context.Exception as EntityNotFoundException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                context.HttpContext.Response.StatusCode = 400;
            }
            else if (context.Exception is InvalidPromotionCodeException)
            {
                var ex = context.Exception as InvalidPromotionCodeException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                context.HttpContext.Response.StatusCode = 400;
            }
            else if (context.Exception != null)
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.detail = stack;

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here

                _logger.LogError("Unhandled exception", apiError.detail);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);
        }
        
        public override void OnException(ExceptionContext context)
        {
            HandleApiException(context);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}
