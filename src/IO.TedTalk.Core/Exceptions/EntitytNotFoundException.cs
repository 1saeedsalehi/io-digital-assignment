namespace IO.TedTalk.Core.Exceptions;

public class EntitytNotFoundException : IOException
{
    public const int ExceptionCode = 5;

    public EntitytNotFoundException(string message) : base(message, ExceptionCode)
    {
    }

    public EntitytNotFoundException(string message, Exception innerException) : base(message, innerException, ExceptionCode)
    {
    }

}
