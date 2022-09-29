namespace LicensePlate.Core.Abstraction;

public abstract class HttpExeptionAbstract : ApplicationException
{
    protected HttpExeptionAbstract() : base()
    {
    }

    protected HttpExeptionAbstract(string massage) : base(massage)
    {
    }

    public abstract int StatusCode { get; }
}