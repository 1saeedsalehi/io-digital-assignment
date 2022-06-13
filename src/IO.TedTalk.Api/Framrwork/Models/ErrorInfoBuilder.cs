namespace IO.TedTalk.Api.Framrwork.Models;

/// <inheritdoc/>
internal class ErrorInfoBuilder : IErrorInfoBuilder
{
    private IExceptionToErrorInfoConverter _converter { get; set; }

    public ErrorInfoBuilder(IExceptionToErrorInfoConverter converter)
    {
        _converter = converter;
    }


    /// <inheritdoc/>
    public ErrorInfo BuildForException(Exception exception)
    {
        var errorInfo = _converter.Convert(exception);
        return errorInfo;
    }

}
