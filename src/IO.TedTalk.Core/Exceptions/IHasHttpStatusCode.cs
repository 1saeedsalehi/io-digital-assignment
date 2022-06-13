using System.Net;

namespace IO.TedTalk.Core.Exceptions;

public interface IHasHttpStatusCode
{
    HttpStatusCode HttpStatusCode { get; }
}
