namespace IO.TedTalk.Api.Framrwork.Models;

/// <inheritdoc/>
internal class ErrorInfoBuilder : IErrorInfoBuilder
{
    private IExceptionToErrorInfoConverter _converter { get; set; }


    /// <inheritdoc/>
    public ErrorInfo BuildForException(Exception exception)
    {
        var errorInfo = _converter.Convert(exception);
        return errorInfo;
    }

}
