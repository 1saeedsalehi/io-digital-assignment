using System.Net;
using System.Text;
using IO.TedTalk.Api.Framrwork.AspNetCore.Config;
using IO.TedTalk.Api.Framrwork.AspNetCore.Reflection;
using IO.TedTalk.Api.Framrwork.Metadata;
using IO.TedTalk.Api.Framrwork.Models;
using IO.TedTalk.Core;
using IO.TedTalk.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.ExceptionHandling;

public class GlobalExceptionFilter : IExceptionFilter
{
    protected readonly ILogger _logger;
    protected readonly IAspnetCoreConfiguration _configuration;
    protected readonly IErrorInfoBuilder _errorInfoBuilder;
    protected readonly ServiceInformation _serviceInfo;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger,
        IAspnetCoreConfiguration configuration, IErrorInfoBuilder errorInfoBuilder, IOptions<ServiceInformation> options)
    {
        _logger = logger;
        _configuration = configuration;
        _errorInfoBuilder = errorInfoBuilder;
        _serviceInfo = options.Value;
    }

    public virtual void OnException(ExceptionContext context)
    {
        var wrapResultAttribute =
            ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                context.ActionDescriptor.GetMethodInfo(),
                _configuration.DefaultWrapResultAttribute
            );

        if (wrapResultAttribute.LogError)
        {
            SeverityAwareLog(context.Exception);
        }

        if (wrapResultAttribute.WrapOnError)
        {
            HandleAndWrapException(context);
        }
    }

    protected virtual void HandleAndWrapException(ExceptionContext context)
    {

        context.HttpContext.Response.StatusCode = GetStatusCode(context);

        bool unathorized = context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized;

        context.Result = new ObjectResult(
            new IOApiResponse(_errorInfoBuilder.BuildForException(context.Exception, _serviceInfo?.NameVersion), unathorized));

        //EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));

        //context.Exception = null; // Handled! // TODO: I'll uncomment it after a while
        context.ExceptionHandled = true;
    }

    protected virtual int GetStatusCode(ExceptionContext context)
    {
        if (context.Exception is IHasHttpStatusCode hasHttpStatusExp)
        {
            return (int)hasHttpStatusExp.HttpStatusCode;
        }

        
        if (context.Exception is IOValidationException)
        {
            return (int)HttpStatusCode.BadRequest;
        }

        
        if (context.Exception is IOInvalidOperationException)
        {
            return (int)HttpStatusCode.BadRequest;
        }

        //if none of above?
        return (int)HttpStatusCode.InternalServerError;
    }

    /// <summary>
    /// TOOD: Utility candidate
    /// </summary>
    /// <param name="exception"></param>
    protected virtual void SeverityAwareLog(Exception exception)
    {
        var logSeverity = LogSeverity.Error;
        string expMessage = exception.Message;
        string expTechMessage = "";
        if (exception is Core.Exceptions.IOException exp)
        {
            logSeverity = exp.Severity;
            expTechMessage = exp.TechnicalMessage;
        }
        // TODO: Move it to NLog configuration file log parameters
        string message = string.Format(
            "Processed an unhandled exception of type {0}:\r\nMessage: {1}\r\nTechnicalMessage: {2}",
            exception.GetType().Name, EscapeForStringFormat(expMessage), EscapeForStringFormat(expTechMessage));
        EventId eventId = exception is Core.Exceptions.IOException aivwaException ? new EventId(aivwaException.ErrorCode ?? 0) : default;

        switch (logSeverity)
        {
            case LogSeverity.Debug:
                _logger.LogDebug(eventId, exception, message);
                break;
            case LogSeverity.Info:
                _logger.LogInformation(eventId, exception, message);
                break;
            case LogSeverity.Warn:
                _logger.LogWarning(eventId, exception, message);
                break;
            case LogSeverity.Error:
                _logger.LogError(exception, message, exception);
                break;
            case LogSeverity.Critical:
                _logger.LogCritical(eventId, exception, message);
                break;
            default:
                _logger.LogWarning(
                    "Invalid parameter passed to SeverityAwareLog method, Please check the code and correct the issue");
                _logger.LogError(eventId, exception, message);
                break;
        }
    }

    protected virtual string EscapeForStringFormat(string input)
    {
        StringBuilder sb = new StringBuilder(input);
        sb.Replace("{", "{{");
        sb.Replace("}", "}}");
        return sb.ToString();
    }
}
