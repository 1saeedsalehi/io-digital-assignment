namespace IO.TedTalk.Api.Framrwork.Models;

/// <summary>
/// This interface is used to build <see cref="ErrorInfo"/> objects.
/// </summary>
public interface IErrorInfoBuilder
{
    /// <summary>
    /// Creates a new instance of <see cref="ErrorInfo"/> using the given <paramref name="exception"/> object.
    /// </summary>
    /// <param name="exception">The exception object</param>
    /// <returns>Created <see cref="ErrorInfo"/> object</returns>
    ErrorInfo BuildForException(Exception exception);

}
