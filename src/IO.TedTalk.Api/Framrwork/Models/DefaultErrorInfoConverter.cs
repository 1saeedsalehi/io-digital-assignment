using IO.TedTalk.Core.Exceptions;
using IO.TedTalk.Core.Extensions;
using System.Text;

namespace IO.TedTalk.Api.Framrwork.Models;

internal class DefaultErrorInfoConverter : IExceptionToErrorInfoConverter
{
    private readonly IWebHostEnvironment _env;

    
    public DefaultErrorInfoConverter(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IExceptionToErrorInfoConverter Next { set; private get; }

    //to eliminate error detail in production environment
    private bool IncludeErrorDetails
    {
        get
        {
            return _env != null ? !_env.IsProduction() : true;
        }
    }

    public ErrorInfo Convert(Exception exception)
    {
        var errorInfo = CreateErrorInfoWithoutCode(exception);

        //for security reason
        if (IncludeErrorDetails)
        {
            var detailBuilder = new StringBuilder();
            CreateDetailsFromException(exception, detailBuilder);
            errorInfo.Details = detailBuilder.ToString();
        }

        if (exception is Core.Exceptions.IOException aivwaException)
        {
            errorInfo.ErrorCode = aivwaException.ErrorCode ?? 0;
        }

        return errorInfo;
    }




    #region Private Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param message="exception"></param>
    /// <returns></returns>
    private ErrorInfo CreateErrorInfoWithoutCode(Exception exception)
    {
        if (exception is AggregateException aggException && exception.InnerException != null)
        {
            if (aggException.InnerException is IOValidationException)
            {
                exception = aggException.InnerException;
            }
        }

        if (exception is IOValidationException validationException)
        {
            return new ErrorInfo(validationException.Message)
            {
                ValidationErrors = GetValidationErrorInfoes(validationException),
                Details = GetValidationErrorNarrative(validationException)
            };
        }

        

        // Finally, check if it's a IOException 
        if (exception is Core.Exceptions.IOException ioException)
        {
            return new ErrorInfo(ioException.Message);
        }

        return new ErrorInfo("InternalServerError");
    }

    private void CreateDetailsFromException(Exception exception, StringBuilder detailBuilder)
    {
        //Exception Message
        detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);

       
        //Additional info for AbpValidationException
        if (exception is IOValidationException)
        {
            var validationException = exception as IOValidationException;
            if (validationException.ValidationErrors.Count > 0)
            {
                detailBuilder.AppendLine(GetValidationErrorNarrative(validationException));
            }
        }

        //Exception StackTrace
        if (!string.IsNullOrEmpty(exception.StackTrace))
        {
            detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
        }

        //Inner exception
        if (exception.InnerException != null)
        {
            CreateDetailsFromException(exception.InnerException, detailBuilder);
        }

        //Inner exceptions for AggregateException
        if (exception is AggregateException aggException)
        {
            if (aggException.InnerExceptions.IsNullOrEmpty())
            {
                return;
            }

            foreach (var innerException in aggException.InnerExceptions)
            {
                CreateDetailsFromException(innerException, detailBuilder);
            }
        }
    }

    private Dictionary<string, string> GetValidationErrorInfoes(IOValidationException validationException)
    {
        var validationErrorInfos = new Dictionary<string, string>();

        int i = 1;
        foreach (var validationResult in validationException.ValidationErrors)
        {
            var validationError = new ValidationErrorInfo(validationResult.ErrorMessage);

            if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
            {
                validationError.Members = validationResult.MemberNames.Select(m => m.ToUpper()).ToArray();
            }

            var key = string.Join(",", validationError.Members) ?? i++.ToString();
            if (!validationErrorInfos.ContainsKey(key))
            {
                validationErrorInfos.Add(key, validationError.Message);
            }
            else
            {
                var value = validationErrorInfos[key];
                validationErrorInfos[key] = value + ". " + validationError.Message;
            }
        }

        return validationErrorInfos;
    }

    private string GetValidationErrorNarrative(IOValidationException validationException)
    {
        var detailBuilder = new StringBuilder();
        detailBuilder.AppendLine("ValidationNarrativeTitle");

        foreach (var validationResult in validationException.ValidationErrors)
        {
            detailBuilder.AppendFormat(" - {0}", validationResult.ErrorMessage);
            detailBuilder.AppendLine();
        }

        return detailBuilder.ToString();
    }

    
    #endregion

}
