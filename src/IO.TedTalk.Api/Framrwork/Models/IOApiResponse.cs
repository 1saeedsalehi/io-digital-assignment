namespace IO.TedTalk.Api.Framrwork.Models;

/// <summary>
/// This class is used to create standard responses for AJAX/remote requests.
/// </summary>
[Serializable]
public class IOApiResponse : IOApiResponse<object>
{
    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object.
    /// <see cref="IOApiResponseBase.Success"/> is set as true.
    /// </summary>
    public IOApiResponse()
    {
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="IOApiResponseBase.Success"/> specified.
    /// </summary>
    /// <param name="success">Indicates success status of the result</param>
    public IOApiResponse(bool success)
        : base(success)
    {
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="IOApiResponse{TResult}.Result"/> specified.
    /// <see cref="IOApiResponseBase.Success"/> is set as true.
    /// </summary>
    /// <param name="result">The actual result object</param>
    public IOApiResponse(object result)
        : base(result)
    {
    }

    /// <summary>
    /// Creates an <see cref="IOApiResponse"/> object with <see cref="IOApiResponseBase.Error"/> specified.
    /// <see cref="IOApiResponseBase.Success"/> is set as false.
    /// </summary>
    /// <param name="error">Error details</param>
    /// <param name="unauthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
    public IOApiResponse(ErrorInfo error, bool unauthorizedRequest = false)
        : base(error, unauthorizedRequest)
    {
    }
}
