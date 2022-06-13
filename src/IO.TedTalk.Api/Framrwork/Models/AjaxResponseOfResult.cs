namespace IO.TedTalk.Api.Framrwork.Models;

/// <summary>
/// This class is used to create standard responses for AJAX requests.
/// </summary>
[Serializable]
public class IOApiResponse<TResult> : IOApiResponseBase
{
    /// <summary>
    /// The actual result object of AJAX request.
    /// It is set if <see cref="IOApiResponseBase.Success"/> is true.
    /// </summary>
    public TResult Result { get; set; }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="Result"/> specified.
    /// <see cref="IOApiResponseBase.Success"/> is set as true.
    /// </summary>
    /// <param name="result">The actual result object of AJAX request</param>
    public IOApiResponse(TResult result)
    {
        Result = result;
        Success = true;
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object.
    /// <see cref="IOApiResponseBase.Success"/> is set as true.
    /// </summary>
    public IOApiResponse()
    {
        Success = true;
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="IOApiResponseBase.Success"/> specified.
    /// </summary>
    /// <param name="success">Indicates success status of the result</param>
    public IOApiResponse(bool success)
    {
        Success = success;
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="IOApiResponseBase.Error"/> specified.
    /// <see cref="IOApiResponseBase.Success"/> is set as false.
    /// </summary>
    /// <param name="error">Error details</param>
    /// <param name="unauthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
    public IOApiResponse(ErrorInfo error, bool unauthorizedRequest = false)
    {
        Error = error;
        UnauthorizedRequest = unauthorizedRequest;
        Success = false;
    }
}
