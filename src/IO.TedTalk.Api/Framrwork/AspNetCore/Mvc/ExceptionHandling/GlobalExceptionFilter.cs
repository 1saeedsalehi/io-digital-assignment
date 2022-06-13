﻿using System.Net;
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


        SeverityAwareLog(context.Exception);



        HandleAndWrapException(context);

    }

    protected virtual void HandleAndWrapException(ExceptionContext context)
    {

        context.HttpContext.Response.StatusCode = GetStatusCode(context);

        bool unathorized = context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized;

        context.Result = new ObjectResult(
            new IOApiResponse(_errorInfoBuilder.BuildForException(context.Exception, _serviceInfo?.NameVersion), unathorized));

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


        if (context.Exception is EntitytNotFoundException)
        {
            return (int)HttpStatusCode.NotFound;
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
        
        if (exception is Core.Exceptions.IOException exp)
        {
            logSeverity = exp.Severity;
        }
        string message = string.Format(
            "Processed an unhandled exception of type {0}:\r\nMessage: {1}",
            exception.GetType().Name, EscapeForStringFormat(expMessage));
        EventId eventId = exception is Core.Exceptions.IOException aivwaException ? new EventId(aivwaException.ErrorCode ?? 0) : default;


        //it can be a security issue
        //we should handle exceptions and eliminate sensetive data from it
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
